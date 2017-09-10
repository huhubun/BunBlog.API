import { Component } from '@angular/core';

export class Hero {
    id: number;
    name: string;
}

@Component({
    selector: 'my-app',
    templateUrl: './my-app.component.html'
})

export class MyApp {
    hero: Hero = {
        id: 1,
        name: "Windstorm"
    }
}
