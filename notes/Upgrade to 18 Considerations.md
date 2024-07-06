# Useful Commands

## Update Node

Easier with a node package manager. Using nvm, take the following steps:

```
nvm install 16.15.1
```

Use the new version:
```
nvm use 16.15.1
```

## A Clean Build

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

## Update Packages

Update typescript
```powershell
npm install typescript@4.7.2 --save-dev --force
```

If you run into the compatibility problem when you clean your `node_modules` Angular may install a higher version of typescript. You can avoid this by making a change like this: `^4.7.2` to `~4.7.2` in your `package.json` for typescript under `devDependencies`.

# Getting back to Angular 14



> Before continuing, it might be in your best interest to make a manual copy backup of the project’s node_modules and take note of the current node version. In case a quick switch is required for other reasons.

At this point, remove all your node_modules to create a complete base.

```powershell
Remove-Item -Path .\node_modules\ -Force -Recurse && Remove-Item -Path .\package-lock.json
# or
Remove-Item -Path .\node_modules\ -Force -Recurse && Remove-Item -Path .\package-lock.json && Remove-Item .\.angular\ -Force -Recurse
```

Do an `npm ls -g @angular/cli` to ensure that your global Angular CLI is compatible, and if not, install it with:
```
npm install -g @angular/cli@14.2.13
```

### Switch to old branch and install packages

Ensure you’re on a branch that has the correct package.json file to install Angular 14 back with the old package versions.

Finally, do a clean install:

```
npm install --legacy-peer-deps
```
### Update Packages

Update typescript
```
npm install typescript@4.7.2 --save-dev --force
```

The next time you clean your node_modules Angular will install a higher version of typescript. You need to avoid this by changing `^4.7.2` to `~4.7.2` in your `package.json` for typescript under devDependencies`. Otherwise you’ll get a higher version of typescript that is incompatible with Angular 14.

Now check the Angular version.

```
ng version
```

You should see that typescript has been updated.

Update `ngx-avatars`.

```
npm install ngx-avatars@1.3.2 --save --force
```

The next time you clean your node_modules Angular will install a higher version of ngx-avatars. You need to avoid this by changing `^1.3.2` to` ~1.3.2` in your package.json

```
"ngx-avatars": "~1.3.2",
```

### Build and serve

Finally try and build everything and it should be good to go.

```
npm run build
npm start
```

# Getting back to Angular 16

```PowerShell
# switch to your living branch (always updated from master)
git switch feature/85643-angular-16-living-2

# switch your node version
nvm use 20.10.0

Remove-Item -Path .\node_modules\ -Force -Recurse && Remove-Item -Path .\package-lock.json

npm install --legacy-peer-deps

# Do build
npm run build
npm start

# Run tests
jest --clearCache && jest --no-cache
```

# Diary

#### You rebase this with the master branch

- [Noticed this page... these components will need to change](https://material.angular.io/guide/mdc-migration)


```
import { LegacyErrorStateMatcher as ErrorStateMatcher} from '@angular/material/legacy-core';
MAT_LEGACY_DATE_FORMATS as MAT_DATE_FORMATS
MatLegacyNativeDateModule as MatNativeDateModule
```

#### 2024-01-23
- Marco asks whether this is in UAT 2. You've stated that you will not deploy it there until you get a gap because it may break other things in UAT 2. [Here is the link to the conversation](https://teams.microsoft.com/l/message/19:7c85e7a1-1329-4adf-8e95-9b87872ca0e4_c6c082c8-3b7a-4250-90ca-ebade140e3df@unq.gbl.spaces/1706008332871?context=%7B%22contextType%22%3A%22chat%22%7D).

#### 2024-01-18 09:41:07 - Merged master into `feature/85643-angular-16-living`.
#### 2024-01-09
- Sreekanth Pogakula notices that there are additions form issues caused by Angular upgrade, specifically to do with [advisor, fee, and tax stepper](https://teams.microsoft.com/l/message/19:r2LJUp7HQKEEzp7B_4MbfYO5mkfzbCINsND1o2JjN3U1@thread.tacv2/1704804605926?tenantId=43b173f2-351a-41f2-a03e-f2af81953f59&groupId=8191302b-d338-46bd-b8de-859a48a2e4f0&parentMessageId=1704804605926&teamName=Ninety%20One%20Digital%20Technology&channelName=General&createdTime=1704804605926).
- Mellisha [finds issues with over here](https://teams.microsoft.com/l/message/19:206dc3cf-f8d3-41b0-b33f-66f1ee3a75ab_7c85e7a1-1329-4adf-8e95-9b87872ca0e4@unq.gbl.spaces/1704808323872?context=%7B%22contextType%22%3A%22chat%22%7D) and [over here](https://teams.microsoft.com/l/message/19:206dc3cf-f8d3-41b0-b33f-66f1ee3a75ab_7c85e7a1-1329-4adf-8e95-9b87872ca0e4@unq.gbl.spaces/1704808572514?context=%7B%22contextType%22%3A%22chat%22%7D).

Problem:
Should be importing like this...
```typescript
import { MatLegacyDialog as MatDialog } from '@angular/material/dialog';
```
but is imported as:
```typescript
import { MatDialog } from '@angular/material/dialog';
```
Since we've upgraded, all dialogs should be referred to like this... it could be, that if Keanu has made changes in this area that he's used the old reference... totally not his fault because he wouldn't have known. Or it could be that Angular missed this for some reason.

There's another exercise (in the future) in the works to update the material controls. But it was too much work for this iteration. All that's really happened with the "legacy-control" is that we've said ok... we're upgrading to Angular 16, but we want to use the old material controls instead of the new ones because we don't have time to upgrade all of them as well.
