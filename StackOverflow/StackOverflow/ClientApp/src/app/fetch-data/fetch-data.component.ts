import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})



export class FetchDataComponent {
  keywords: string;
  forums: Forums[];
  baseUrl: string;


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  Search()
  {
    const body = JSON.stringify(this.keywords);
    this.http.post<Forums[]>(this.baseUrl + 'api/StackOverflow/GetSearchResults', body, httpOptions).subscribe(result => {
      this.forums = result;
    }, error => console.error(error));
  }
}

interface Forums {
  Title: string;
  Description: string;
  Votes: string;
  Answers: string;
  UserCreated: string;
  Reputation: string;
  Badges: string;
}
