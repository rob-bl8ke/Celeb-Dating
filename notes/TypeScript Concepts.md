
#### Use Types rather than Interfaces

```typescript
type User = {
    name: string
    age: number
}
```
vs...

```typescript
interface User {
    name: string
    age: string
}
```

- [I Cannot Believe TypeScript Recommends You Do This](https://www.youtube.com/watch?v=oiFo2z8ILNo)- Web Dev simplified runs through the reasoning of why you should rather use types. This guy misses the point of interfaces, however.In "classical" OO languages, use Types for when a thing "IS-A" other thing.  Interfaces are for when a thing "BEHAVES-LIKE" other thing.