import { inject } from 'aurelia-dependency-injection';

import { CommandCoordinator } from '@dolittle/commands';

import { QueryCoordinator } from '@dolittle/queries';
import { AllProjects } from './Projects/AllProjects';
import { ImprovementsForProject } from './Projects/ImprovementsForProject';

@inject(CommandCoordinator, QueryCoordinator)
export class welcome {
    projects=[];

    improvements=[];

    constructor(commandCoordinator, queryCoordinator) {
        let self = this;
        this._commandCoordinator = commandCoordinator;
        this._queryCoordinator = queryCoordinator;


        var query = new AllProjects();
        this._queryCoordinator.execute(query).then(result => {
            result.items.forEach(_ => self.projects.push(_));
        });
    }

    submit() {
    }

    getBuilds(id)
    {
        let self = this;
        var query = new ImprovementsForProject();
        query.project = id;
        this._queryCoordinator.execute(query).then(result => {
            debugger;
            result.items.forEach(_ => self.improvements.push(_));
        });

    }
}