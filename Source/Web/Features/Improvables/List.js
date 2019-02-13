import { QueryCoordinator } from '@dolittle/queries';
import { AllImprovables } from './AllImprovables';
import { inject } from 'aurelia-dependency-injection';

@inject(QueryCoordinator)
export class List {
    improvables=[];

    constructor(queryCoordinator) {
        let self = this;
        this._queryCoordinator = queryCoordinator;

        var query = new AllImprovables();
        this._queryCoordinator.execute(query).then(result => {
            self.improvables = result.items;
        });
    }
}