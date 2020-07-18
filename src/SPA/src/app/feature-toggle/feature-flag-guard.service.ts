import { EsquioService, FeatureToggle } from './esquio-service';
import { CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { Injectable } from '@angular/core';


@Injectable()
export class FeatureFlagGuardService implements CanActivate {
    
    constructor(private esquioService: EsquioService) { }
    
    canActivate(route: ActivatedRouteSnapshot): Promise<boolean> | boolean {
        const featureName = route.data['featureName'];

        return new Promise((resolve, reject) => {
            this.esquioService.esquio([featureName]).subscribe(
                (featureToggles: FeatureToggle[]) => {
                    let featureToggle = featureToggles.find(feature => feature.name == featureName);
                    console.log(`Feature toggle: ${featureToggle.enabled}`)
                    resolve(featureToggle.enabled);
                },
                error => {
                    reject(error);
                },
                () => {
                    resolve(false);
                }
            );
        });
    }
}