import { QueryCoordinator } from '@dolittle/queries';
import { AllProjects } from './AllProjects';
import { inject } from 'aurelia-dependency-injection';

@inject(QueryCoordinator)
export class List {
    projects=[];

    constructor(queryCoordinator) {
        let self = this;
        this._queryCoordinator = queryCoordinator;

        var query = new AllProjects();
        this._queryCoordinator.execute(query).then(result => {
            self.projects = result.items;
        });
    }
}