/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { QueryCoordinator } from '@dolittle/queries';
import { inject } from 'aurelia-dependency-injection';
import { StepResultsForStep } from './StepResultsForStep';

@inject(QueryCoordinator)
export class ImprovementDetails {
    #queryCoordinator;
    results = [];

    constructor(queryCoordinator) {
        this.#queryCoordinator = queryCoordinator;
    }

    async activate(params) {
        let query = new StepResultsForStep();
        query.project = params.id;
        query.version = params.version;
        query.number = params.step;

        let result = await this.#queryCoordinator.execute(query);
        this.results = result.items;
    }
}