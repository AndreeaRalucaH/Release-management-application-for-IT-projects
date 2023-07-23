import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthguardserviceService } from 'app/services/authguardservice.service';
import jwt_decode from 'jwt-decode';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginCard: boolean = true;

  constructor(private router: Router,
    private service: AuthguardserviceService,
    private _ngZone: NgZone,) { }

  ngOnInit(): void {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: "45378756417-3ftbflfpdmu3og14qp8jv9fi7qj2od07.apps.googleusercontent.com",
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,

      });
      
      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById("buttonDiv"),
        { theme: "outline", size: "large" }
      );
      // @ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => {});
    }

  }

  async handleCredentialResponse(response: any) {
    console.log(jwt_decode(response.credential))
    if (response) {
      this.loginCard = false;
      localStorage.setItem("googleAuth",response.credential);
      this.router.navigate(['home/home/relfollowup'])
        .then(() => {
          window.location.reload();
        });
    }
  }

}
