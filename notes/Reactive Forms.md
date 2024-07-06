# Angular Reactive Forms

Created: November 4, 2023 8:24 AM

# Basic Setup and Submit

With the reactive approach, the form is driven from the component and not the template.

```html
<div class="container">
  <div class="row">
    <div class="col-xs-12 col-sm-10 col-md-8 col-sm-offset-1 col-md-offset-2">
      <form [formGroup]="signupForm" (ngSubmit)="onSubmit()">
        <div class="form-group">
          <label for="username">Username</label>
          <input
            type="text"
            id="username"
            formControlName="username"
            class="form-control">
        </div>
        <div class="form-group">
          <label for="email">email</label>
          <input
            type="text"
            id="email"
            formControlName="email"
            class="form-control">
        </div>
        <div class="radio" *ngFor="let gender of genders">
          <label>
            <input
              type="radio"
              formControlName="gender"
              [value]="gender">{{ gender }}
          </label>
        </div>
        <button class="btn btn-primary" type="submit">Submit</button>
      </form>
    </div>
  </div>
</div>
```

Take note of how the form is synchronized between the template and the component using the `[formGroup]` property binding and the `formControlName` which simply references the name of the `FormControl` instance within the `FormGroup` of the component.

Carefully notice how the radio buttons are generated and how the `FormGroup` is created with default values.

Notice how you get access to the form in code. You don’t use a template reference variable you simply access the `signupForm` in the `onSubmit` method. The `(ngSubmit)="onSubmit()` in the template is how the `onSubmit` method is invoked on the component.

```tsx
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  genders = ['male', 'female'];
  signupForm: FormGroup;
  
  ngOnInit(): void {
    this.signupForm = new FormGroup({
      'username': new FormControl(null),
      'email': new FormControl(null),
      'gender': new FormControl('female')
    });
  }

  onSubmit() {
    console.log(this.signupForm)
  }
}
```

# Built-in Validation

There are a number of built in validators that you can use and as the reactive approach is imperatively driven, you will add these validators to your `FormControl` constructor parameters.

```tsx
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  genders = ['male', 'female'];
  signupForm: FormGroup;
  
  ngOnInit(): void {
    this.signupForm = new FormGroup({
      'username': new FormControl(null, Validators.required),
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'gender': new FormControl('female')
    });
  }

  onSubmit() {
    console.log(this.signupForm)
  }
}
```

With this in place here is an example of how this will change the way your form control is declared in the template. Take note of the `span` element which now uses the component `signupForm` to access the applicable control and check whether the control is invalid and has been touched. One can then display the text under the control to inform the user where the correction needs to be made.

```html
<div class="form-group">
  <label for="email">email</label>
  <input
    type="text"
    id="email"
    formControlName="email"
    class="form-control">
    <span *ngIf="!signupForm.get('email').valid && signupForm.get('username').touched" class="help-block">Please enter a valid email.</span>
</div>
```

You can add the following to the CSS in order to style the control in its invalid state.

```css
input.ng-invalid.ng-touched {
  border: 1px solid red;
}
```

Another thing to keep in mind is that you can do the same thing with the entire form in the template. The entire form has a valid and touched status…

```html
<div class="container">
  <div class="row">
    <div class="col-xs-12 col-sm-10 col-md-8 col-sm-offset-1 col-md-offset-2">
      <form [formGroup]="signupForm" (ngSubmit)="onSubmit()">

        ...
        
        <span *ngIf="!signupForm.valid && signupForm.touched" class="help-block">Please enter valid data.</span>
        <button class="btn btn-primary" type="submit">Submit</button>
      </form>
    </div>
  </div>
</div>
```

# Nested Form Groups

Note that one can have nested form groups for more complicated data form representations. The example below is of a `signupForm` which has an outer group and an inner group (`userData`). You can nest to more levels if you’d like.

```tsx
ngOnInit(): void {
    this.signupForm = new FormGroup({
      'userData': new FormGroup({
        'username': new FormControl(null, Validators.required),
        'email': new FormControl(null, [Validators.required, Validators.email]),
      }),
      'gender': new FormControl('female')
    });
  }
```

This needs to be represented correctly in the template. This is the way you’ll do it.

```html
<form [formGroup]="signupForm" (ngSubmit)="onSubmit()">
  <div formGroupName="userData">
    <div class="form-group">
      <label for="username">Username</label>
      <input
        type="text"
        id="username"
        formControlName="username"
        class="form-control">
        <span *ngIf="!signupForm.get('userData.username').valid && signupForm.get('userData.username').touched" class="help-block">Please enter a valid username.</span>
    </div>
    <div class="form-group">
      <label for="email">email</label>
      <input
        type="text"
        id="email"
        formControlName="email"
        class="form-control">
        <span *ngIf="!signupForm.get('userData.email').valid && signupForm.get('userData.username').touched" class="help-block">Please enter a valid email.</span>
    </div>
  </div>
  <div class="radio" *ngFor="let gender of genders">
    <label>
      <input
        type="radio"
        formControlName="gender"
        [value]="gender">{{ gender }}
    </label>
  </div>
  <span *ngIf="!signupForm.valid && signupForm.touched" class="help-block">Please enter valid data.</span>
  <button class="btn btn-primary" type="submit">Submit</button>
</form>
```

Take a close look at the template. The two form-group `div` elements are now nested within a new `div`. Note how we set the `formGroupName` rather than a `formControlName`.

```html
<div formGroupName="userData">...</div>
```

Take a close look at the `span` validation information elements and note that a path is used to reference the nested group’s control.

```html
<span *ngIf="!signupForm.get('userData.username').valid && signupForm.get('userData.username').touched" class="help-block">Please enter a valid username.</span>
```

# Dynamic Controls

In the component below, the “hobbies” property has been added to the form as a new `FormArray`. Its been initialized to a new array as it will be dynamically added to the form by dynamically creating input controls in code.

In order to create controls on the fly, create a button and on its click event create  new `FormControl`.  Add this control to the hobbies form array.

The template will need to access it so use a getter method called `controls()` to expose the hobbies controls to the template.

```tsx
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  genders = ['male', 'female'];
  signupForm: FormGroup;

  get controls() {
    return (this.signupForm.get('hobbies') as FormArray).controls;
  }
  
  ngOnInit(): void {
    this.signupForm = new FormGroup({
      'userData': new FormGroup({
        'username': new FormControl(null, Validators.required),
        'email': new FormControl(null, [Validators.required, Validators.email]),
      }),
      'gender': new FormControl('female'),
      'hobbies': new FormArray([])
    });
  }

  onSubmit() {
    console.log(this.signupForm)
  }

  onAddHobby() {
    const control = new FormControl(null, Validators.required);
    (this.signupForm.get('hobbies') as FormArray).push(control);
  }
}
```

The template part looks like this. Note how a `div` with the `formArrayName` set to “hobbies” has been added. Now, iterate over the controls property exposed by the component and name the control based on its index.

```html
<form [formGroup]="signupForm" (ngSubmit)="onSubmit()">
  ...
  <div formArrayName="hobbies">
    <h4>
      Your Hobbies
    </h4>
    <button class="btn btn-default" type="button" (click)="onAddHobby()">Add Hobby</button>
    <div class="form-group" *ngFor="let hobbyControl of controls; let i = index">
      <input type="text" class="form-control" [formControlName]="i">
    </div>
  </div>
  ...
</form>
```

# Custom Validators

Here a custom validator has been added for the `username` control. We have a list of forbidden user names that should never be available for use. The user should not try to submit one of these names in our fictitious scenario.

```tsx
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  genders = ['male', 'female'];
  signupForm: FormGroup;
  forbiddenUserNames = ['Paul', 'Anna'];

  get controls() {
    return (this.signupForm.get('hobbies') as FormArray).controls;
  }
  
  ngOnInit(): void {
    this.signupForm = new FormGroup({
      'userData': new FormGroup({
        'username': new FormControl(null, [Validators.required, this.forbiddenNames.bind(this)]),
        'email': new FormControl(null, [Validators.required, Validators.email]),
      }),
      'gender': new FormControl('female'),
      'hobbies': new FormArray([])
    });
  }

  onSubmit() {
    console.log(this.signupForm)
  }

  onAddHobby() {
    const control = new FormControl(null, Validators.required);
    (this.signupForm.get('hobbies') as FormArray).push(control);
  }

  // This is a custom validator function
  // The return value is TypeScripts way of declaring for a key value pair.
  // The key is a string and the value is a boolean.
  forbiddenNames(control: FormControl): {[s: string]: boolean} {
    if (this.forbiddenUserNames.indexOf(control.value) !== -1) {
      return { 'nameIsForbidden': true };
    }

    // If validation passes, always return null
    return null;
  }
}
```

There are a couple of things going on here, so lets take a look at them in turn.

💡 When you made the component changes you may run into “Cannot read property ‘forbiddenUserNames’ of undefined” error. You’ve run into a “this” gotcha. Think about where the function `forbiddenNames` is being called from. Its not called from within the component class, its called from within the Angular framework.

Since Angular calls the validator and not the component, the `bind` trick has to be used in order to ensure that `this` refers to the correct `this`.

```tsx
'username': new FormControl(null, [Validators.required, this.forbiddenNames.bind(this)]),
```

A couple of things need to be cleared up about the validator function… take a good look at it.

```tsx
forbiddenNames(control: FormControl): {[s: string]: boolean} {
  if (this.forbiddenUserNames.indexOf(control.value) !== -1) {
    return { 'nameIsForbidden': true };
  }
  return null;
}
```

- Note that `{[s: string]: boolean}` is TypeScript’s way to declare a key-value pair with the key of type string, and the value of type Boolean. This is what will be returned from the validator method.
- Note that if the validation passes, a null return value is returned and not an object.
- `indexOf` returns a -1 if a control value is not present in the array. However, -1 is interpreted as true and in this case it should be interpreted as false. Hence, the strong equality is required.

# Use Error Codes to be “Specific”

Submit while the form is invalid and take a look at the form group object that has been logged to the console. Find the `controls` property and start drilling down into the controls of the various nested group and their controls. Look for the `errors` collections and you’ll see the list of errors based on their “error IDs”. If your custom validation is failing you should see `nameIsForbidden` as an error in the `username` control errors. If the field is empty but required you should see a `required` error.

Use this to your advantage to be more specific about the validation errors to display the correct message to the user.

```tsx
<div class="form-group">
  <label for="username">Username</label>
  <input
    type="text"
    id="username"
    formControlName="username"
    class="form-control">
    <span *ngIf="!signupForm.get('userData.username').valid && signupForm.get('userData.username').touched" 
    class="help-block">
    <span *ngIf="signupForm.get('userData.username').errors['nameIsForbidden']">This name is forbidden.</span>
    <span *ngIf="signupForm.get('userData.username').errors['required']">This name is required.</span>
  </span>
</div>
```

# Asynchronous Custom Validators

Asynchronous custom validators work slightly differently and add an additional state to valid state of a control. The `ng-pending` state is applied to the control (and hence the form group) when the validator is waiting for a promise to resolve or an observable to emit.

Here is how the validator is created in the component.

```tsx
forbiddenEmails(control: FormControl): Promise<any> | Observable<any> {
  const promise = new Promise<any>((resolve, reject) => {
    setTimeout(() => {
      if (control.value === 'test@test.com') {
        resolve({'emailIsForbidden': true });
      } else {
        resolve(null);
      }
    }, 1500);
  });

  return promise;
}
```

Here, we’re just dealing with it as a promise, but it still need to be able to return both a promise and an observable. The `setTimeout` is simply there to simulate a response from another server. Similar to a synchronous validator, the `resolve()` method will either return an object with an error code or null if the validation passed.

The validator must be applied to the correct control in the form group and notice that asynchronous validators are supplied to the third parameter, not the second. If you have more than one, use an array.

```tsx
'email': new FormControl(null, [Validators.required, Validators.email], this.forbiddenEmails.bind(this)),
```

### Listen to status changes on your controls

Angular Forms provide two useful observables that allow you track your value changes and status changes. In particular, status changes in this context is very useful as this observable will log the status changes as they occur on your form as you type or change stuff… you’ll see your control change for “INVALID” to “PENDING” (if async) to “VALID” or whatever.

```tsx
ngOnInit(): void {
    ...
    // this.signupForm.valueChanges.subscribe(value => console.log(value));
    this.signupForm.statusChanges.subscribe(value => console.log(value));
  }
```

# Setting an patching programmatically

Note that you can set or patch your form at any time.

Setting requires all values to exist whereas patching allows for the modification of only certain form input control values.

You can also reset your form to its initial pristine state.

```tsx
ngOnInit(): void {
    ...
    
  this.signupForm.setValue({
    'userData': {
      'username': 'Lisa',
      'email': 'lisa@test.com'
    },
    'gender': 'female',
    'hobbies': []
  });

  this.signupForm.patchValue({
    'userData': {
      'username': 'Jennifer',
      'email': 'jeniffer@test.com'
    },
  });

  // this.signupForm.reset();
}
```

# There’s more

Accessing the form group programmatically:

- You can add or remove validators dynamically.
- Check whether a validator exists.
- Mark as pristine, dirty, touched…
- Amongst others…