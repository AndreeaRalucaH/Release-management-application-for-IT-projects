import { Release } from "./release";

export class Medii {
    idmediu: number = 0;
    denumire: string = '';
    datacreare: Date = new Date();
    datamodificare: Date = new Date();
    releases: Release[] = [];
}
