import { HttpHeaders } from "@angular/common/http";

export class GlobalVarible {
    static host: string = "https://hackathoncoin.azurewebsites.net/api/Hack";
    static username: string = "BugLord";

    static httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
}