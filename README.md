<a name="readme-top"></a>
[![MIT License][license-shield]][license-url]

<p>
  <a href="https://github.com/helsingborg-stad/gdi-cases-server">
    <img src="docs/images/hbg-github-logo-combo.png" alt="Logo" width="300">
  </a>
</p>
<h1>GDI Cases Server</h1>
<p>
  <a href="https://github.com/helsingborg-stad/gdi-cases-server/issues">Report Bug</a>
  ·
  <a href="https://github.com/helsingborg-stad/gdi-cases-server/issues">Request Feature</a>
</p>



# 

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
				<li><a href="#configure-environment-variables">Configure environment variables</a></li>
				<li><a href="#setup-a-datalayer">Setup a datalayer</a></li>
				<li><a href="#graphql-explorer">GraphQL explorer</a></li>
        <li><a href="#integrations">Integrations</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Cases services for GDI.

- REST endpoint for uploading federated case descriptions

The general reasoning behind this solution is that
- a citizen can expect to be given a normalized, standardized and homogenized view of her involvment with a go (a Citverning entityy for example)
- the government has internallyu a plethora of various processes and software systems supporting dialogs (i.e. cases) with citizens
-  Onboarding of case management systems should NOT rely on direct, ad-hoc runtime integration but rather frequent imports of current state in respective case management system
-  Each case managament system exporter is responsible for
   -  publishing intervals
   -  data safety and governance (GDPR etc)
  
<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Configure environment variables

In order to run, some environment variables must be defined. This is easiest to do by
- ensuring there exists a local `.env` in the project root
- copy content from [.env.example](./gdi-cases-server/.env.example) and change values to what gives meaning

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Setup a datalayer

- [MongoDB as datalayer](./gdi-cases-server/Modules/Cases/MongoDb/README.md)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Integrations

- [MongoDB as datalayer](gdi-cases-server/Modules/Cases/MongoDb/README.md)


<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Roadmap

- [ ] Improve testcoverage (MongoDB being an external dependency is currently a obstacle)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Best-README-Template](https://github.com/othneildrew/Best-README-Template)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[license-shield]: https://img.shields.io/github/license/helsingborg-stad/gdi-cases-server.svg?style=for-the-badge
[license-url]: https://github.com/helsingborg-stad/gdi-cases-server/blob/master/LICENSE.txt