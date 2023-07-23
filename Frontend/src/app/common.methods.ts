import jwt_decode from 'jwt-decode';
import { UtilizatoriService } from './services/utilizatori.service';
export class CommonMethods {

    constructor() { }

    checkIsAdmin = async (userService: UtilizatoriService): Promise<boolean> => {
        const userTkn = localStorage.getItem("googleAuth");
        const userDetails = jwt_decode(userTkn);
        // @ts-ignore
        const userEmail = await userService.getOneUserByDen(userDetails.email).toPromise();
        console.log(userEmail)
        if (userEmail.esteadmin == 1) {
            return true;
        } else {
            return false;
        }
    }
}