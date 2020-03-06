
<div align=center><img src="resources/toolbox.png" width="200"/></div>

![Github](https://img.shields.io/badge/Github-build-yellowgreen.svg?style=flat-square)
![Github](https://img.shields.io/badge/Nuget-v0.1,1-yellowgreen.svg?style=flat-square)

# DuSolidWorksExtension

English | [中文](https://github.com/weianweigan/DuSolidWorksExtension/blob/master/README.cn.md)

Lots of very useful extension methods for SolidWorks api.

Based on https://github.com/Weingartner/SolidworksAddinFramework


## Install

```
PM> Install-Package Du.SolidWorks -Version 0.1.1
```

## Usage

### 1. Add namespace firstly

```
using Du.SolidWorks.Extension;
using Du.SolidWorks.Math
```

### 2. You can use almost extension methods 

* EquatiomMgr Extension Methods

 ![](resources/equExtension.png)


```csharp
var doc = _addin.SwApp.ActiveDoc as ModelDoc2;

var equ = doc.GetEquationMgr().GetAllEqu().
          Where(p => p.GlobalVariable).Select(p => p.VarName);
```

----------------------------------------------------------------------------
----------------------------------------------------------------------------

* CustomPropertyManager Extension Methos 

![](resources/cusExtension.png)

```csharp
var doc = _addin.SwApp.ActiveDoc as ModelDoc2;

var dateProerty = doc.Extension.CustomPropertyManager[""].GetAllProperty()
                ?.Where(p => p.Value.Contains("日期"))?.Select(p => p.Name);
```

## Document 

* Document is being written

* [Click here to See Auto Generated Document](https://github.com/weianweigan/DuSolidWorksExtension/blob/master/src/Du.SolidWorks.Doc/Help/index.html)

* interfaces 

![](resources\tree.png)