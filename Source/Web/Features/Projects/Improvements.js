/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { observable, containerless } from 'aurelia-framework';
import { QueryCoordinator } from '@dolittle/queries';
import { ImprovementsForProject } from './ImprovementsForProject';
import { StepsForImprovement } from './StepsForImprovement';
import { inject } from 'aurelia-dependency-injection';

@containerless()
@inject(QueryCoordinator)
export class Improvements {
    #queryCoordinator;
    #router;

    improvements = [];
    @observable selectedVersion;

    steps = [];

    #firstRun = true;
    #project;

    constructor(queryCoordinator) {
        this.#queryCoordinator = queryCoordinator;
    }

    async activate(params, routeConfig, navigationInstruction) {
        let query = new ImprovementsForProject();
        query.project = params.id;
        this.#project = params.id;

        this.selectedVersion = navigationInstruction.plan.default.childNavigationInstruction.params.version;
        this.#firstRun = false;
        let result = await this.#queryCoordinator.execute(query);
        this.improvements = result.items;

        this.populateSteps();
    }

    populateSteps() {
        let query = new StepsForImprovement();
        query.project = this.#project;
        query.version = this.selectedVersion;

        this.#queryCoordinator.execute(query).then(result => {
            this.steps = result.items;
        });
    }

    configureRouter(config, router) {
        this.#router = router;

        config.title = 'Improvement details';
        config.map([{
            route: ['', ':version'],
            name: 'Details',
            moduleId: PLATFORM.moduleName('Projects/ImprovementDetails')
        }]);
    }

    async selectedVersionChanged(version) {
        if (this.#firstRun ) return;
        this.#router.navigate(version);
    }
}