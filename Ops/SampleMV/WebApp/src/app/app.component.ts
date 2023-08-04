import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'WebApp';

  weMode = 'Config DB';
  configUrl = environment.apiUrl + '/api/Configuration/GetWeatherOptions'

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<any>(this.configUrl).subscribe(
      (data) => {
        debugger;
        this.weMode = data.mode;
      },
      (error) => {
        console.error('An error occurred:', error);
      }
    );
  }

}
