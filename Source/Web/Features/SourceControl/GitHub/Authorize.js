import { QueryCoordinator } from '@dolittle/queries';
import { CommandCoordinator } from '@dolittle/commands';
import { Guid } from '@dolittle/core';
import { inject } from 'aurelia-dependency-injection';
import { RegisterInstallation } from './RegisterInstallation';
import { AllAvailableRepositories } from './AllAvailableRepositories';

@inject(QueryCoordinator, CommandCoordinator)
export class Authorize {
    installationId = '';
    repositories = [];

    constructor(queryCoordinator, commandCoordinator) {
        this._queryCoordinator = queryCoordinator;
        this._commandCoordinator = commandCoordinator;
    }

    activate() {
        const query = new AllAvailableRepositories();
        this._queryCoordinator.execute(query).then(result => {
            this.repositories = result.items[0].repositories;
        });
    }

    register() {
        console.log('Register:', this.installationId);

        const command = new RegisterInstallation();
        command.id = Guid.create();
        command.githubId = this.installationId;

        this._commandCoordinator.handle(command).then(result => {
            console.log('Result:', result);
        });

    }
}