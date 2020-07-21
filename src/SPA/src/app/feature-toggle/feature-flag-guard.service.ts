import { EsquioService, FeatureToggle } from './esquio-service';
import { CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';


@Injectable()
export class FeatureFlagGuardService implements CanActivate {

  constructor(private esquioService: EsquioService) { }

  canActivate(route: ActivatedRouteSnapshot): Promise<boolean> | boolean {
    const featureName = route.data['featureName'];

    return this.esquioService
    .esquio([featureName])
    .pipe<boolean>(map(features => features.find(feature => feature.name == featureName).enabled))
    .toPromise();
  }
}
