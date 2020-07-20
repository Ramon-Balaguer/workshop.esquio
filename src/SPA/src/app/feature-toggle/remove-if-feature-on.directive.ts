import { Directive, TemplateRef, Input, ViewContainerRef } from '@angular/core';
import { EsquioService, FeatureToggle } from './esquio-service';
import {NgIf} from '@angular/common';


@Directive({
  selector: '[removeIfFeatureOn]'
})
export class RemoveIfFeatureOnDirective extends NgIf {
  @Input('removeIfFeatureOn') featureName: string;

  constructor(
              private templateRef: TemplateRef<any>,
              private viewContainer: ViewContainerRef,
              private esquioService: EsquioService) {
                super(viewContainer, null);
  }

  ngOnInit() {
    this.esquioService.esquio([this.featureName]).subscribe((featureToggles: FeatureToggle[])=>{
      let featureToggle = featureToggles.find(feature => feature.name == this.featureName);
      console.log(`Feature toggle: ${featureToggle.enabled}`)
      if (!featureToggle.enabled) {
          this.viewContainer.clear();
      }else
      {
        this.viewContainer.createEmbeddedView(this.templateRef);
      }
    });
  }
}
