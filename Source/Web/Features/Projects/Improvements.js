import {customElement, containerless} from 'aurelia-framework';
import { QueryCoordinator } from '@dolittle/queries';
import { ImprovementsForProject } from './ImprovementsForProject';
import { inject } from 'aurelia-dependency-injection';

@containerless()
@inject(QueryCoordinator)
export class Improvements {
    improvements=[];

    constructor(queryCoordinator) {
        this._queryCoordinator = queryCoordinator;
    }

    activate(params) {
        let self = this;
        var query = new ImprovementsForProject();
        query.project = params.id;
        this._queryCoordinator.execute(query).then(result => {
            self.improvements = result.items;
        });     
    }

    configureRouter(config, router) {
        this.router = router;
        config.title = 'Improvement details';
        config.map([{
            route: ['',':version'],
            name: 'Details',
            moduleId: PLATFORM.moduleName('Projects/ImprovementDetails')
        }]);
    }
}