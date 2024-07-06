# Simple Upgrade from 17 (end of Section 3)

```powershell
nvm install lts
nvm use 20.15.0
npm ls -g
npm install -g @angular/cli@18
npm ls -g
ng version
cd .\client\
ng version
ng update @angular/core@18 @angular/cli@18
```
Also need to upgrade the following third party packages.
```powershell
npm update ngx-bootstrap
Remove-Item -Path .\node_modules\ -Force -Recurse && Remove-Item -Path .\package-lock.
npm i
```

# Helpful CLI knowledge

### Update Node

Easier with a node package manager. Using nvm, take the following steps:

```
nvm install 16.15.1
```

Use the new version:
```
nvm use 16.15.1
```

### A Clean Build

Do an `npm ls -g @angular/cli` to ensure that your global Angular CLI is compatible, and if not, install it with:
```powershell
npm install -g @angular/cli@14.2.13
# or
npm install --location=global @angular/cli@16.2.13`
```
Confirm that it is installed locally with `npm ls -g`. Now, do a clean build by completely remove node modules and clean up package lock file.

```powershell
Remove-Item -Path .\node_modules\ -Force -Recurse && Remove-Item -Path .\package-lock.json
npm install --legacy-peer-deps

npm run build
npm run test
```
Only if using Jest, you can clear the cache

```powershell
jest --clearCache && jest --no-cache
jest
```

### Update Packages

Update typescript
```powershell
npm install typescript@4.7.2 --save-dev --force
```

If you run into the compatibility problem when you clean your `node_modules` Angular may install a higher version of typescript. You can avoid this by making a change like this: `^4.7.2` to `~4.7.2` in your `package.json` for typescript under `devDependencies`.

