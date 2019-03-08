import { QueryCoordinator } from '@dolittle/queries';
import { CommandCoordinator } from '@dolittle/commands';
import { inject } from 'aurelia-dependency-injection';
import { computedFrom } from 'aurelia-framework';
import { AllInstallations } from './AllInstallations';
import { RegisterInstallation } from './RegisterInstallation';
import { UnregisterInstallation } from './UnregisterInstallation';


@inject(QueryCoordinator, CommandCoordinator)
export class Installations {
    registeredInstallationIds = [];
    availableInstallations = [];

    constructor(queryCoordinator, commandCoordinator) {
        this._queryCoordinator = queryCoordinator;
        this._commandCoordinator = commandCoordinator;
    }

    activate() {
        this._queryCoordinator.execute(new AllInstallations()).then(result => {
            if (result.items.length) {
                this.registeredInstallationIds = result.items[0].installations;
            }
        });

        this.fetchInstallationsForUser().then(result => {
            this.availableInstallations = result;
            console.log('INSTALLATIONS', result);
        });
    }

    @computedFrom('availableInstallations','registeredInstallationIds')
    get allInstallations() {
        return this.availableInstallations.map(installation => {
            return {
                ...installation,
                Registered: this.registeredInstallationIds.indexOf(installation.Id) >= 0,
            };
        });
    }

    /*
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
    */

    fetchInstallationsForUser() {
        return fetch(process.env.BASE_URL+'/thirdparty/github/userauth/installations').then(response => {
            if (response.status == 401) {
                location = process.env.BASE_URL+'/thirdparty/github/userauth/initiate?callback='+encodeURIComponent(location.href);
            } else {
                return response.json();
            }
        });
    }

    triggerRegister(installationId, event) {
        event.target.disabled = true;
        event.target.innerHTML = 'Adding...'

        const cmd = new RegisterInstallation();
        cmd.id = installationId;
        this._commandCoordinator.handle(cmd).then(result => {
            if (result.success) {
                this.registeredInstallationIds = this.registeredInstallationIds.concat(installationId)
                event.target.innerHTML = 'Add'
                event.target.disabled = false;
            }
        });
    }

    triggerUnregister(installationId, event) {
        event.target.disabled = true;
        event.target.innerHTML = 'Removing...'

        const cmd = new UnregisterInstallation();
        cmd.id = installationId;
        this._commandCoordinator.handle(cmd).then(result => {
            if (result.success) {
                this.registeredInstallationIds = this.registeredInstallationIds.filter(id => id != installationId);
                event.target.innerHTML = 'Remove'
                event.target.disabled = false;
            }
        });
    }
}