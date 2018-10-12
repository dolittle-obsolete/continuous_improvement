import { PLATFORM } from 'aurelia-pal';

export function configure(config) {
  config.globalResources(PLATFORM.moduleName('./checklist_item/checklist_item'));
  config.globalResources(PLATFORM.moduleName('./menu_item/menu_item'));
  config.globalResources(PLATFORM.moduleName('./side_bar/side_bar'));
}
