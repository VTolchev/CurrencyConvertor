import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConvertorService } from './convertor.service';
import { FormsModule } from '@angular/forms';

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

  convert( ) {
    this.conversionResult = "Conversion result";
    return "Conversion result";
  }
    
}
