

```
Dorisoy.Rigger
├─ .vscode
├─ Dory.Rigger.MAUI.sln
├─ DoryRiggerMaui
│  ├─ App.xaml
│  ├─ MainPage.xaml
│  ├─ Pages
│  ├─ Platforms
│  │  ├─ Android
│  │  ├─ iOS
│  │  │  └─ Info.plist
│  │  ├─ MacCatalyst
│  │  │  └─ Info.plist
│  │  ├─ Tizen
│  │  └─ Windows
│  │     ├─ app.manifest
│  │     └─ App.xaml
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerServer
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerWasm
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ LICENSE
├─ README.md
├─ ReadMe.txt
├─ Screen.png
├─ SharedServices
└─ SharedUI
   ├─ cssStyles
   ├─ Models
   └─ Pages

```
```
Dorisoy.Rigger
├─ Dory.Rigger.MAUI.sln
├─ DoryRiggerMaui
│  ├─ Pages
│  ├─ Platforms
│  │  ├─ Android
│  │  ├─ iOS
│  │  │  └─ Info.plist
│  │  ├─ MacCatalyst
│  │  │  └─ Info.plist
│  │  ├─ Tizen
│  │  └─ Windows
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerServer
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerWasm
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ LICENSE
├─ README.md
├─ Screen.png
├─ SharedServices
└─ SharedUI
   ├─ cssStyles
   ├─ Models
   └─ Pages

```
```
Dorisoy.Rigger
├─ Dory.Rigger.MAUI.sln
├─ DoryRiggerMaui
│  ├─ Pages
│  ├─ Platforms
│  │  ├─ Android
│  │  ├─ iOS
│  │  │  └─ Info.plist
│  │  ├─ MacCatalyst
│  │  │  └─ Info.plist
│  │  ├─ Tizen
│  │  └─ Windows
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerServer
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerWasm
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ LICENSE
├─ README.md
├─ Screen.png
├─ SharedServices
└─ SharedUI
   ├─ cssStyles
   ├─ Models
   └─ Pages

```
=======
# Dorisoy.Rigger
基于 .net core 6 + MAUI + Blazor 实现的开发框架生成器，支持多数据库，自定义T4模板

# 数据库适配

SQLServer

MySQL

PostgreSQL

Oracle

SQlLite


# 解决方案

```
Dorisoy.Rigger
├─ Dory.Rigger.MAUI.sln
├─ DoryRiggerMaui    //MAUI
│  ├─ Pages
│  ├─ Platforms
│  │  ├─ Android
│  │  ├─ iOS
│  │  ├─ MacCatalyst
│  │  ├─ Tizen
│  │  └─ Windows
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerServer  //Balzor Server
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ DoryRiggerWasm   //Balzor Wasm
│  ├─ Pages
│  ├─ Properties
│  └─ Shared
├─ SharedServices   //Service
└─ SharedUI         //Shared
   ├─ cssStyles
   ├─ Models
   └─ Pages
```

# T4 模板

```
Default
├─ .vs
│  ├─ Dory.MicroService
│  │  └─ v17
│  │     └─ .suo
│  ├─ nkv.MicroService
│  │  ├─ DesignTimeBuild
│  │  │  └─ .dtbcache.v2
│  │  └─ FileContentIndex
│  │     ├─ bc7907e5-6d79-44da-8931-0bedf149a452.vsidx
│  │     ├─ merges
│  │     └─ read.lock
│  └─ ProjectEvaluation
│     ├─ nkv.microservice.metadata.v5.1
│     └─ nkv.microservice.projects.v5.1
├─ build
│  ├─ ModuleBuildTargets.cs
│  ├─ ModuleBuildTargets.Extend.cs
│  └─ ModuleBuildTargets.tt
├─ DefaultCodeGenerateEngine.cs
├─ Dockerfile.cs
├─ Dockerfile.Extend.cs
├─ Dockerfile.tt
├─ Gitignore.cs
├─ Gitignore.Extend.cs
├─ Gitignore.tt
├─ Readme.cs
├─ Readme.Extend.cs
├─ Readme.tt
├─ Solution.cs
├─ Solution.Extend.cs
├─ Solution.tt
└─ src
   ├─ Core
   │  ├─ Application
   │  │  ├─ Dto
   │  │  │  ├─ AddDto.cs
   │  │  │  ├─ AddDto.Extend.cs
   │  │  │  ├─ AddDto.tt
   │  │  │  ├─ QueryDto.cs
   │  │  │  ├─ QueryDto.Extend.cs
   │  │  │  ├─ QueryDto.tt
   │  │  │  ├─ UpdateDto.cs
   │  │  │  ├─ UpdateDto.Extend.cs
   │  │  │  └─ UpdateDto.tt
   │  │  ├─ ServiceImpl.cs
   │  │  ├─ ServiceImpl.Extend.cs
   │  │  ├─ ServiceImpl.tt
   │  │  ├─ ServiceInterface.cs
   │  │  ├─ ServiceInterface.Extend.cs
   │  │  └─ ServiceInterface.tt
   │  ├─ Csproj.cs
   │  ├─ Csproj.Extend.cs
   │  ├─ Csproj.tt
   │  ├─ Domain
   │  │  ├─ Entity.cs
   │  │  ├─ Entity.Extend.cs
   │  │  ├─ Entity.tt
   │  │  ├─ EntityEnum.cs
   │  │  ├─ EntityEnum.Extend.cs
   │  │  ├─ EntityEnum.tt
   │  │  ├─ EntityExtend.cs
   │  │  ├─ EntityExtend.Extend.cs
   │  │  ├─ EntityExtend.tt
   │  │  ├─ RepositoryInterface.cs
   │  │  ├─ RepositoryInterface.Extend.cs
   │  │  └─ RepositoryInterface.tt
   │  ├─ Infrastructure
   │  │  ├─ CacheKeys.cs
   │  │  ├─ CacheKeys.Extend.cs
   │  │  ├─ CacheKeys.tt
   │  │  ├─ Config.cs
   │  │  ├─ Config.Extend.cs
   │  │  ├─ Config.tt
   │  │  ├─ DbContext.cs
   │  │  ├─ DbContext.Extend.cs
   │  │  ├─ DbContext.tt
   │  │  ├─ Localizer.cs
   │  │  ├─ Localizer.Extend.cs
   │  │  ├─ Localizer.tt
   │  │  ├─ ModuleServicesConfigurator.cs
   │  │  ├─ ModuleServicesConfigurator.Extend.cs
   │  │  ├─ ModuleServicesConfigurator.tt
   │  │  └─ Repositories
   │  │     ├─ RepositoriesImpl.cs
   │  │     ├─ RepositoriesImpl.Extend.cs
   │  │     └─ RepositoriesImpl.tt
   │  └─ Resources
   │     ├─ LocalizerEn.cs
   │     ├─ LocalizerEn.Extend.cs
   │     └─ LocalizerEn.tt
   ├─ DirectoryBuildProps.cs
   ├─ DirectoryBuildProps.Extend.cs
   ├─ DirectoryBuildProps.tt
   ├─ UI
   │  └─ Web
   │     ├─ build
   │     │  ├─ AppConfigJs.cs
   │     │  ├─ AppConfigJs.Extend.cs
   │     │  ├─ AppConfigJs.tt
   │     │  ├─ BaseConfigJs.cs
   │     │  ├─ BaseConfigJs.Extend.cs
   │     │  ├─ BaseConfigJs.tt
   │     │  ├─ LibConfigJs.cs
   │     │  ├─ LibConfigJs.Extend.cs
   │     │  ├─ LibConfigJs.tt
   │     │  ├─ LocalesConfigJs.cs
   │     │  ├─ LocalesConfigJs.Extend.cs
   │     │  └─ LocalesConfigJs.tt
   │     ├─ Env.cs
   │     ├─ Env.Extend.cs
   │     ├─ Env.tt
   │     ├─ EnvDevelopment.cs
   │     ├─ EnvDevelopment.Extend.cs
   │     ├─ EnvDevelopment.tt
   │     ├─ Eslintrc.cs
   │     ├─ Eslintrc.Extend.cs
   │     ├─ Eslintrc.tt
   │     ├─ IndexHtml.cs
   │     ├─ IndexHtml.Extend.cs
   │     ├─ IndexHtml.tt
   │     ├─ PackageJson.cs
   │     ├─ PackageJson.Extend.cs
   │     ├─ PackageJson.tt
   │     ├─ Prettierrc.cs
   │     ├─ Prettierrc.Extend.cs
   │     ├─ Prettierrc.tt
   │     ├─ src
   │     │  ├─ IndexJs.cs
   │     │  ├─ IndexJs.Extend.cs
   │     │  ├─ IndexJs.tt
   │     │  ├─ locales
   │     │  │  ├─ en
   │     │  │  │  ├─ Index.cs
   │     │  │  │  ├─ Index.Extend.cs
   │     │  │  │  └─ Index.tt
   │     │  │  └─ zh_cn
   │     │  │     ├─ Index.cs
   │     │  │     ├─ Index.Extend.cs
   │     │  │     └─ Index.tt
   │     │  ├─ MainJs.cs
   │     │  ├─ MainJs.Extend.cs
   │     │  ├─ MainJs.tt
   │     │  └─ store
   │     │     ├─ StoreJs.cs
   │     │     ├─ StoreJs.Extend.cs
   │     │     └─ StoreJs.tt
   │     ├─ VsCodeConfig.cs
   │     ├─ VsCodeConfig.Extend.cs
   │     └─ VsCodeConfig.tt
   ├─ Web
   │  ├─ Controllers
   │  │  ├─ Controller.cs
   │  │  ├─ Controller.Extend.cs
   │  │  └─ Controller.tt
   │  ├─ Csproj.cs
   │  ├─ Csproj.Extend.cs
   │  ├─ Csproj.tt
   │  ├─ ModuleController.cs
   │  ├─ ModuleController.Extend.cs
   │  ├─ ModuleController.tt
   │  └─ _modules
   │     ├─ ModuleJson.cs
   │     ├─ ModuleJson.Extend.cs
   │     └─ ModuleJson.tt
   └─ WebHost
      ├─ Appsettings.cs
      ├─ Appsettings.Extend.cs
      ├─ Appsettings.tt
      ├─ AppsettingsDevelopment.cs
      ├─ AppsettingsDevelopment.Extend.cs
      ├─ AppsettingsDevelopment.tt
      ├─ AppsettingsLocal.cs
      ├─ AppsettingsLocal.Extend.cs
      ├─ AppsettingsLocal.tt
      ├─ Csproj.cs
      ├─ Csproj.Extend.cs
      ├─ Csproj.tt
      ├─ Program.cs
      ├─ Program.Extend.cs
      ├─ Program.tt
      └─ Properties
         ├─ LaunchSettings.cs
         ├─ LaunchSettings.Extend.cs
         └─ LaunchSettings.tt

```

<img src="https://github.com/dorisoy/Dorisoy.Rigger/blob/main/Screen.png?raw=true" />
