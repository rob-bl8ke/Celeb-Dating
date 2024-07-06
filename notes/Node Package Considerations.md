# `ngx-bootstrap`

- [Udemy Bookmark](https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/learn/lecture/44315048#overview)

Avoid using the Angular schematics way of installing third party packages as they are slow to catch up on Angular. In so saying, prefer Angular components rather than plain old JavaScript component as these components tend to work better with Angular concepts such as change detection, etc. So to do a [manual install](https://valor-software.com/ngx-bootstrap/#/documentation#installation) follow these steps:

- Execute `npm install ngx-bootstrap@12 bootstrap font-awesome`. Note its fairly important to know what version of `ngx-bootstrap` is compatible with your current version of Angular.
- [See the official Change Log](https://github.com/valor-software/ngx-bootstrap/blob/development/CHANGELOG.md) for version compatibility guidance.
- Find the styles array in `angular.json` and add the relative paths to the `node_module` style sheets.

```json
{
  "projects": {
    "client": {
      "architect": {
        "build": {
          "builder": "@angular-devkit/build-angular:browser",
          "options": {
            "outputPath": "dist/client",
            "index": "src/index.html",
            "main": "src/main.ts",
            "polyfills": "src/polyfills.ts",
            "tsConfig": "tsconfig.app.json",
            "assets": ["src/favicon.ico", "src/assets"],
            "styles": [
              // here...
              "node_modules/ngx-bootstrap/datepicker/bs-datepicker.css",
              "node_modules/bootstrap/dist/css/bootstrap.min.css",
              "node_modules/bootswatch/dist/united/bootstrap.css",
              "node_modules/font-awesome/css/font-awesome.min.css",
              "node_modules/ngx-toastr/toastr.css",
              "node_modules/ngx-spinner/animations/line-scale-party.css",
              "src/styles.css"
            ],
            "scripts": []
          },
        },
      }
    }
  }
}
```
Restart the Angular App to check if you've made a typo. The terminal will give you an error. If you were successful you'd see a different font in your browser window and if you go to the developer tools and look under Sources for your styles css file you should see this:

```css
/* src/styles.css */

/* node_modules/bootstrap/dist/css/bootstrap.min.css */
/*!
* Bootstrap  v5.3.3 (https://getbootstrap.com/)
* Copyright 2011-2024 The Bootstrap Authors
* Licensed under MIT (https://github.com/twbs/bootstrap/blob/main/LICENSE)
*/
```

### IMPORTANT (Review for Angular 18)
When you upgrade to Angular 18 review this [material again](https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/learn/lecture/44315048#overview).
 
