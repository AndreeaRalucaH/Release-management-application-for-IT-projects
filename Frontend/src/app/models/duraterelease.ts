import { Release } from "./release";

export class Duraterelease {
    iddurata: number = 0;
    luna: string = '';
    saptamana: string = '';
    datarelease: string = '';
    durata: string = '';
    downtime: string = '';
    datastart:string = '';
    dataend:  string = '';
    datacreare: Date = new Date();
    datamodificare: Date = new Date();
    releases: Release[] = [];
}
