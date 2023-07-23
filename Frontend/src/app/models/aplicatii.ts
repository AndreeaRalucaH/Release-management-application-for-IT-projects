import { Release } from "./release";

export class Aplicatii {
    idaplicatie: number = 0;
    denumire: string = '';
    emails: string = '';
    ownerproiect: string = '';
    managerproiect: string = '';
    datacreare: Date = new Date();
    datamodificare: Date = new Date();
    releases: Release[] = [];
}
