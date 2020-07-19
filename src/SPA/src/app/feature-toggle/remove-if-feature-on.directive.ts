import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { EsquioService, FeatureToggle } from './esquio-service';

@Directive({
  selector: '[removeIfFeatureOn]'
})
export class RemoveIfFeatureOnDirective implements OnInit {
  @Input('removeIfFeatureOn') featureName: string;

  constructor(private el: ElementRef,
              private esquioService: EsquioService) {
  }

  ngOnInit() {
    this.esquioService.esquio([this.featureName]).subscribe((featureToggles: FeatureToggle[])=>{
      let featureToggle = featureToggles.find(feature => feature.name == this.featureName);
      console.log(`Feature toggle: ${featureToggle.enabled}`)
      if (!featureToggle.enabled) {
          this.el.nativeElement.parentNode.removeChild(this.el.nativeElement);
      }
    });
  }
}