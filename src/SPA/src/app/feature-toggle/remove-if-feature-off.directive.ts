import { Directive, TemplateRef, Input, ViewContainerRef } from '@angular/core';
import { EsquioService, FeatureToggle } from './esquio-service';
import {NgIf} from '@angular/common';


@Directive({
  selector: '[removeIfFeatureOff]'
})
export class RemoveIfFeatureOffDirective extends NgIf {
  @Input('removeIfFeatureOff') featureName: string;


  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private esquioService: EsquioService) {
      super(viewContainer, templateRef);
  }

  ngOnInit() {
    this.esquioService.esquio([this.featureName]).subscribe((featureToggles: FeatureToggle[])=>{
    let featureToggle = featureToggles.find(feature => feature.name == this.featureName);
    console.log(`Feature toggle: ${featureToggle.enabled}`)
    if (featureToggle.enabled) {
        this.viewContainer.clear();
    }else
    {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
    });
  }
}
