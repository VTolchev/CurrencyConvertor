import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConvertorService } from './convertor.service';
import { FormsModule } from '@angular/forms';
import { ConvertResponse } from '../dto/convert-response.model';
import { ConvertRequest } from '../dto/convert-request.model';

@Component({
  selector: 'app-convertor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './convertor.component.html',
  styleUrl: './convertor.component.css'
})
export class ConvertorComponent {
  convertorService : ConvertorService = inject(ConvertorService);
  
  value: string = '';
  conversionResult : string = '';

  convert() {
    const observer = {
      next: (response: ConvertResponse) => {
        this.conversionResult = response.conversionResult ;
      },
      error: (err: any) => {
        console.error(err);
        this.conversionResult = err.message;
      }
    };

    //todo: validate input

    const request: ConvertRequest =
    {
      currencyCode: "USD",
      languageCode: "en",
      value: this.value
    }

    this.convertorService.convert(request).subscribe(observer);
  }
}
