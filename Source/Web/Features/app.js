import { PLATFORM } from 'aurelia-pal';
import style from '../styles/style.scss';
import { inject } from 'aurelia-dependency-injection';

export class app {
    constructor() {
    }

    configureRouter(config, router) {
        config.options.pushState = true;
        config.map([
            { route: ['', 'Projects/List'], name: 'Project List', moduleId: PLATFORM.moduleName('Projects/List') },
            { route: 'Projects/Improvements/:id', name: 'Improvements', moduleId: PLATFORM.moduleName('Projects/Improvements') }
        ]);

        this.router = router;
    }
}
