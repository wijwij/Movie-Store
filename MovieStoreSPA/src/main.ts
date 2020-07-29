import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}
/**
 * main.ts will bootstrp the application and calls AppModule.
 * Every angular app should have at least one Module, the root module, by default it is AppModule
 *
 * main --> AppModule --> AppComponent
 */

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
