# BuyingCatalogueEmailClient

![.NET Core](https://github.com/nhs-digital-gp-it-futures/BuyingCatalogueEmailClient/workflows/.NET%20Core/badge.svg)

This repo contains the Buying Catalogue Email Client library, which contains the functionality to send an e-mail to a recipient. The library is packaged as a NuGet package available in the Buying Catalogue MyGet feed.

## Installing the package

### Prerequisites

Make sure the solution has a `.nuget` solution directory with a `NuGet.config` file that has the following contents.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="MyGet" value="https://www.myget.org/F/buying-catalogue/api/v3/index.json" />
    <add key="NuGet.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

### Installing the latest version

Run the following commmand in the package manager console.

```powershell
Install-Package NHSD.BuyingCatalogue.EmailClient
```

### Install a specific version

Run the following command in the package manager console.

```powershell
Install-Package NHSD.BuyingCatalogue.EmailClient -Version 1.2.3
```

Where `1.2.3` is replaced with the relevant version number.
