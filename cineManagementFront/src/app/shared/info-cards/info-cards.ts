import { Component, Input } from '@angular/core';
import { CardInfo } from '../../models/cardInfo.interface';

// Angular Material Modules
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-info-cards',
  imports: [
    MatCardModule,
    MatIconModule,
    MatProgressBarModule,
    MatButtonModule,
  ],
  templateUrl: './info-cards.html',
  styleUrl: './info-cards.css'
})
export class InfoCards {
  @Input() titulo = '';
  @Input() valor = '';
}
