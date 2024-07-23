# What are Observables?

> Use for asynchronous streaming and pub-sub type operations. HTTP calls.

- New standard for managing async data included in ES7 (ES2016).
- Introduced in Angular v2
- Observables are lazy collections of multiple values over time
- Think of Observables like a newsletter.
    - Only subscribers receive the newsletter
    - If no one subscribes the newsletter will not be printed.

| Promise | Observable |
| --- | --- |
| Provides a single future value | Emits multiple values over time |
| Not lazy | Lazy (will not emit unless someone subscribes) |
| Can not cancel | Able to cancel |
|  | Can use with map, filter, reduce, and other operators |

# Signals

> Use for synchronous state management.

- Signals are not used for asynchronous code. We still need Observables for that.
- However you no longer need to use Observables to inform components of a change within another component.
- Simple and readable. Syntax is more declarative and makes our code easier to understand.
- Less boilerplate than Observables.
- Performant.
    - More efficient than observables in terms of change detection.
    - Automatically cleaned up. Less risk of memory leaks.
- Predictable state change.
- Well integrated with Angular components.

A signal is a wrapper around a value that notifies interested consumers when that value changes. Signals can contain any value, from primitives to complex data structures.

```typescript
const count = signal(0);

// signals are getter functions - calling them reads their value
console.log('The count is: ' + count());

// set a new value
count.set(3);

// update a value
count.update(value => value + 2);
```

Excellent for synchronous state management in an application.

### Application

- Logging in and logging out users (in this application).


```typescript
  currentUser = signal<User | null>(null);
  
  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
```
```typescript
  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
```

```
    @if (accountService.currentUser()) {
      <ul class="navbar-nav me-auto mb-2 mb-md-0">
        ...
      </ul>
    }
```