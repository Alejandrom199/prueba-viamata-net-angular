import { Component, OnInit } from '@angular/core';
import { InfoCards } from "../../../shared/info-cards/info-cards";
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CardInfo } from '../../../models/cardInfo.interface';
import { SalaCineService } from '../../../services/sala-cine-service';
import { DashboardDTO } from '../../../models/dtos/dashboard.dto';

@Component({
  selector: 'app-dashboard-metricas',
  imports: [InfoCards,
    MatCardModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './dashboard-metricas.html',
  styleUrl: './dashboard-metricas.css'
})
export class DashboardMetricas implements OnInit{

  dataDashboard!: DashboardDTO

  constructor(
    private salaCineService: SalaCineService
  ){}

  ngOnInit(): void {
    this.obtenerDashboardData();
  }

  obtenerDashboardData(){
    this.salaCineService.obtenerDashboardData().subscribe(data =>
      this.dataDashboard = data
    )
  }

}
