import { PLATFORM } from 'aurelia-pal';
import style from '../styles/style.scss';
import { inject } from 'aurelia-dependency-injection';

export class app {
    constructor() {
    }

    configureRouter(config, router) {
        config.options.pushState = true;
        config.map([
            { route: ['', 'Improvables'], name: 'Improvables', moduleId: PLATFORM.moduleName('Improvables/List') },
            { route: 'Improvements/:improvable', name: 'Improvements', moduleId: PLATFORM.moduleName('Improvements/Index') },

            { route: ['GitHub/', 'GitHub/Repositories', 'GitHub/Repositories/:status/:id?'], name: 'GitHub - Repositories', moduleId: PLATFORM.moduleName('SourceControl/GitHub/Repositories') },
            { route: ['GitHub/Installations/'], name: 'GitHub - Installations', moduleId: PLATFORM.moduleName('SourceControl/GitHub/Installations') },
        ]);

        this.router = router;
    }
}
