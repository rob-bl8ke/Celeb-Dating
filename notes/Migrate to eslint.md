
# Replacing tslint with eslint

### Uninstall these packages first to get a clean install

Currently one has to install using `npm install --legacy-peer-deps`. To enable a successful install without having to use `--legacy-peer-deps`, the following packages must be uninstalled in order:

- `npm uninstall @angular/flex-layout --force`
- `npm uninstall angular-resize-event --force`
- `npm uninstall ngx-loading --force`
- `npm uninstall ngx-take-until-destroy --force`
- `npm uninstall stylelint-config-prettier --force`
- `npm uninstall tslint-plugin-prettier --force`

At this point you have a clean `npm` install going (although your app won't build because you've removed a number of important dependencies). However, this will allow you to run the following command:

### Add the necessary package

```
ng add @angular-eslint/schematics
```
Running this not only adds the package but does a bit of configuration in order to prepare your Angular solution for `eslint`.
- Creates a `.eslintrc.json` file with settings.
- You may need to create a `.prettierignore` file.
- Makes changes to `angular.json` file.
- Changes to your `settings.json` file in your `.vscode` folder.

```json
{
  "name": "client-app",
  "version": "0.0.0",
  "scripts": {
    ...
    "lint": "ng lint && npm run lint:styles",
    ...
    "prettier": "prettier 'src/**' --write",
    ...
  },
  "private": true,
  ...
  "devDependencies": {
    "@angular-devkit/build-angular": "^16.2.6",
    "@angular-eslint/builder": "16.3.1",
    "@angular-eslint/eslint-plugin": "16.3.1",
    "@angular-eslint/eslint-plugin-template": "16.3.1",
    "@angular-eslint/schematics": "16.3.1",
    "@angular-eslint/template-parser": "16.3.1",
    ...
    "@typescript-eslint/eslint-plugin": "5.62.0",
    "@typescript-eslint/parser": "5.62.0",
    ...
    "eslint": "^8.51.0",
    "eslint-config-prettier": "^9.1.0",
    "eslint-plugin-prettier": "^5.1.0",
    ...
  }
}

```

Now, don't bother running `ng g @angular-eslint/schematics:convert-tslint-to-eslint --remove-tslint-if-no-more-tslint-targets` as this will not work.. You'll get an error: "Error: The `convert-tslint-to-eslint` schematic is no longer supported."

```
npm run lint
ng lint --fix
```
### Other actions

- Delete `tslint.json`
- Remove all tslint packages.
- Install `prettier-elint` plugins to get prettier to play nicely with eslint.

```
npm install prettier-eslint eslint-config-prettier eslint-plugin-prettier --save-dev
```

### Reinstall the packages

Reinstall the packages you uninstalled at the beginning of the process...

- `npm install @angular/flex-layout --force`
- `npm install angular-resize-event --force`
- `npm install ngx-loading --force`
- `npm install ngx-take-until-destroy --force`
- `npm install stylelint-config-prettier --force`
- `npm install tslint-plugin-prettier --force`

# References

- [Migrate from tslint to eslint](https://github.com/angular-eslint/angular-eslint/blob/main/docs/MIGRATING_FROM_TSLINT.md)
- [Configure Prettier and ESLint with Angular](https://itnext.io/configure-prettier-and-eslint-with-angular-e7b4ce979cd8)
- [Painless Migration of tslint to eslint — Angular](https://medium.com/@bhavinmatariya99/painless-migration-of-tslint-to-eslint-angular-b25da240320c)
