import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { HSaaSComponent } from './components/h-saa-s.component';
import { HSaaSRoutingModule } from './h-saa-s-routing.module';

@NgModule({
  declarations: [HSaaSComponent],
  imports: [CoreModule, ThemeSharedModule, HSaaSRoutingModule],
  exports: [HSaaSComponent],
})
export class HSaaSModule {
  static forChild(): ModuleWithProviders<HSaaSModule> {
    return {
      ngModule: HSaaSModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<HSaaSModule> {
    return new LazyModuleFactory(HSaaSModule.forChild());
  }
}
