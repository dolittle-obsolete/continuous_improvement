import { customElement, containerless, bindable } from 'aurelia-framework';

@customElement('menu-item')
@containerless()
export class menu_item {
    @bindable
    nav_url;
    @bindable
    icon_name;
    @bindable
    text;

    constructor() {}

    get icon_url() {
        let icon_file = '';
        if (this.icon_name) {
            icon_file = require(`../../../assets/icons/${this.icon_name}.svg`);
        }
        return icon_file;
    }
}
