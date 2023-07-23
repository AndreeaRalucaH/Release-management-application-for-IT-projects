import { Release } from "./release";

export class Utilizatori {
    idutilizator: number = 0;
    nume: string = '';
    email: string = '';
    esteadmin: number = 0;
    datacreare: Date = new Date();
    datamodificare: Date = new Date();
    releases: Release[] = [];
}
