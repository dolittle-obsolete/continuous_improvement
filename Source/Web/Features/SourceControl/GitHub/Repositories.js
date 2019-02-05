import { QueryCoordinator } from '@dolittle/queries';
import { CommandCoordinator } from '@dolittle/commands';
import { Guid } from '@dolittle/core';
import { inject } from 'aurelia-dependency-injection';
import { AllAvailableRepositories } from './AllAvailableRepositories';
import { AllInstallations } from './AllInstallations';
import { TriggerUpdateOfRepositories } from './TriggerUpdateOfRepositories';

@inject(QueryCoordinator, CommandCoordinator)
export class Repositories {
    installationId = '';
    repositories = [];
    errorMessage;
    successMessage;

    constructor(queryCoordinator, commandCoordinator) {
        this._queryCoordinator = queryCoordinator;
        this._commandCoordinator = commandCoordinator;
    }

    activate(params) {
        if (params.status == 'InstallationSuccess') {
            this.successMessage = 'GitHub repositories added successfully! It might take a moment to appear in the list below.';
        } else if (params.status == 'InstallationError') {
            this.errorMessage = 'Something went wrong during the installation!';
        } else if (params.status == 'InstallationUpdated') {
            this.successMessage = 'GitHub repositories updated! It might take a moment to refresh the list below.';
        } else if (params.status == 'SelectInstallations') {
            this.selectInstallations();
        }

        const query = new AllAvailableRepositories();
        this._queryCoordinator.execute(query).then(result => {
            if (result.items.length) {
                this.repositories = result.items[0].repositories;
            }
        });
    }

    triggerUpdate(event) {
        event.target.disabled = true;
        event.target.innerHTML = 'Updating...'

        this._queryCoordinator.execute(new AllInstallations()).then(result => {
            if (result.success && result.items.length) {
                const installationIds = result.items[0].installations;
                const cmd = new TriggerUpdateOfRepositories();
                cmd.installationIds = installationIds;
                this._commandCoordinator.handle(cmd).then(result => {
                    if (result.success) {
                        event.target.innerHTML = 'It might take a minute to refresh...'
                    }
                });
            }
        });
    }

    selectInstallations() {
        fetch('http://localhost:5000/thirdparty/github/userauth/installations').then(response => {
            if (response.status == 401) {
                location = 'http://localhost:5000/thirdparty/github/userauth/initiate?callback='+encodeURIComponent('http://localhost:8080/GitHub/Repositories/SelectInstallations');
            } else {
                response.json().then(data => {
                    console.log('Available installations', data);
                });
            }
        });
    }
}