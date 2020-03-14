<p align="center">
  <img src="/doc/logo.png" />  
</p>

# Backend Test
## _Sergi Collado Figueras - Marzo 2020_

#### Online en: https://backendtestapi20200313053942.azurewebsites.net/swagger/index.html

Implementación del escenario descrito en la prueba de test Backend para __Beezy__.

Se construye una REST API en .NetCore 3.1 que provee de los endpoints correspondientes a los escenarios descritos:

- __Ejercicio 1__: se implementa la estructura de los controllers para todos los endpoints descritos. Aactualmente devuelven un _error 404_ con el texto _Under construction..._.

- __Ejercicio 2__: se implementa el endpoint _/api/managers/billboards/intelligent-billboard_ con la funcionalidad completa tanto para búsqueda en BD, como para búsqueda en _API themoviedb.org_. Actualmente devuelve un _Billboard_, paginado, con la información obtenida en la búsqueda.

### Diseño

El contexto y modelos de la base de datos se ha obtenido inicialmente mediante un _scaffold_ de la base de datos. Se ha optado por llevar dicho modelo a un dominio mucho más _DDD_ (al menos por lo que respecta a los patrones tácticos), a pesar de las limitaciones que se podrian encontrar si la aplicación tuviese que efectuar operaciones de escritura en el repositorio. En ese caso, y dado que el diseño de la base de datos viene dado, hubiese sido aceptable mantener el conjunto de modelos (_Dtos_) obtenido directamente. Pero, para el ejercicio, se ha optado por modificar tanto los modelos para convertirlos en entidades de dominio (con _ValueObjects_) y el contexto para mapear el nuevo diseño resultante de forma funcional. Además, este enfoque presenta los siguientes _trade-offs_:

* Los Id's de las entidades son de tipo _integer_ (viene marcado por el modelo original) pero en un dominio _DDD 'greenfield'_ hubiesemos usado el tipo _Guid_. En este, se hace la trampa Guid.GetHashcode() para introducir la generación de identidades en el dominio, agnóstico de la infrastructura. Pero esto podria dar error (aunque como nunca guardamos en BD una nueva _Entity_, es suficiente para trabajar "en teoria" el dominio _DDD_ lo más purista posible).

* A pesar de que se encapsula todo el modelo en entidades, se siguen dejando dichas entidades algo anémicas. Se deberia implementar la gestión de colecciones en las entidades que lo requiriesen, ya que al tratarse de un proyecto sólo de lectura no es necesario, pero en un modelado 100% _DDD_ debería resolverse.

* Los _ValueObjects_ se han desgranado a bastante nivel semántico. Podría haberse optado por agrupar más los conceptos, pero la granularidad permite conceptualizar mejor el significado de cada _ValueObject_, así como particularizar mejor las validaciones de invariantes que deben cumplir.

* Los _ValueObjects_ permiten evitar _Primitive Obssession_, es decir, permiten que los métodos de las entidades y servicios de dominio sean honestos (no tienen que realizar comprobaciones sobre los valores de los parametros), y que el propio compilador compruebe que cada parámetro está en el orden que le corresponde (lo que evita errores de programación).

La solución se implementa siguiendo al máximo una arquitectura _Clean_ por capas, en las que la infrastructura (_Cross-cutting_ incluido) queda en la capa exterior, la _API_ orquestra el flujo hacia el dominio (_Controllers_ y servicios de aplicación) y el dominio (entidades y agregados, _ValueObjects_, servicios de negocio) es agnóstico del resto de capas.

Se resuelve el problema del versionado mediante _namespace_ (por directorio), puesto que es la forma más sencilla de versionar clases, controladores, _Dtos_ cuando una _API_ crece considerablemente.

### Implementación

* Se ha implementado la solución usando _TDD_ para _Entidades_, _ValueObjects_, _Servicios_, _Handlers_, algoritmo de cálculo de _Billboards_...

* Los _commits_ permiten seguir la implementación de la solución en sus distintas fases. Se ha utilizado _Semantic Commit Messages_ para formalizar la literatura de los comentarios.

* Se han aplicado los principios _SOLID_, _YAGNI_, _KISS_ y _DRY_, además de respetar las _Transformation Priority Premises_, las _4 Rules of Simple Design_ y, en general, generando código de la forma más _Clean_ posible.

* Se ha utilizado el patrón _Mediator_ (implementado por el nuget _MediatR_) para desacoplar los controladores de sus dependencias. Sólo dependen de _MediatR_, que es quien se encarga de hacer llegar una _request_ al _handler_ correspondiente, y devolver la _response_ si procede. De este modo, las inyecciones de dependencias no sobrecargan los controladores, y se puede extender la aplicación muy fácilmente creando _handlers_ nuevos, evitando modificar código de forma colateral (respetando el principio _Open/Close_ de _SOLID_). Además, _MediatR_ provee de _PipeLines_ para configurar _logging_ y las validaciones de las _requests_. Con _MediatR_ también resulta muy sencillo enfocar el código a _CQS_ (_Command Query Segregation_).

* Se usa el patrón _Repository_ para no depender directamente de la infrastructura de datos.

* Se usa el paquete _FluentValidation_ para facilitar las validaciones que se efectuan en dichos _PipeLines_ de validación.

* Se usa el paquete _Serilog_ como implementación de _ILogger_. Dado que se utiliza el _PipeLine_ de _MediatR_ no es necesario inyectar el _logger_ para conseguir que la aplicación registre la actividad.

* Se usa el paquete _AspNetCoreRateLimit_ para configurar el _throttle_ de la _API_, limitando así el número de llamadas por cliente. Esto evita saturaciones y ataques _DoS_. En la ejecución en local de la _API_ la limitación no actua, debido a los _white lists_ configurados en _appconfig.cs_, pero en la versión _OnLine_ se ha configurado la limitación a 50 peticiones cada 15 minutos para que sea fácil de ver cómo responde la _API_ al sobrepasar el límite de peticiones.

* Se usa el patrón _MayBe_ de programación funcional, implementado por el paquete _Optional_, para lidiar con la mayoria de valores _null_ y evitar los errores en los _edge cases_.

* Se usa el paquete _Scrutor_ para facilitar las configuraciones de los validadores.

* Se usa el paquete _Hellang.Middleware.ProblemDetails_ para gestionar el tipo de respuesta de los controladores en función de las excepciones lanzadas por los servicios que se ejecutan.

* Se usa _Swagger_ para documentar y permitir testear la _REST API_. A pesar de que se implementan varios controladores, se agrupan de forma que _Swagger_ muestre sólo 2 conceptos: _Viewers_ y _Managers_.

* Se usa el paquete _RestSharp_ para conectar con la _API_ externa de _themoviedb.org_.

* En el apartado de _testing_ se utiliza el framework _xUnit_ junto con _FluentAssertions_, _NSubstitute_ para _mocking_ y _AutoFixture_ para generar datos de test.

### Consideraciones finales y mejoras

* Actualmente tanto la _ConnectionString_ como la _API Key_ para las conexiones a datos se almacenan en plano en el fichero _appconfig.json_. En este caso no sucede nada (excesivamente) grave debido a la naturaleza de dicho acceso a datos, pero en caso de querer implementar una mejora en este punto, se hubiese usado la gestion de secretos de _dotnet_.

* La paginación implementada resuelve la cantidad de datos que se le pasa al cliente, pero no la cantidad de datos que se leen de los orígenes de datos. Esto es así por la naturaleza del algoritmo creado. Aun así, para el caso de la base de datos, y teniendo separado las lecturas de las escrituras (_CQS/CQRS_), se podria optar por el uso de un _Micro ORM_ como _Dapper_ para que las operaciones de lectura fuesen mucho más óptimas, si el volumen de carga de datos fuese elevado.

* Se podria haber usado el paquete _Polly_ para establecer las políticas de _Retry_ y _Circuit Breaker_ de accesos externos (base de datos y _API themoviedb.org_).

* Se deberian haber implementado tests de integración que testeasen la _API_ "en vivo". Esto es posible mediante una _Factory_ que cree una _API_ con un contexto _EF InMemory_ hidratado por código, lo que permite la ejecución de tests "reales" sin necesidad de infrastructura. Si vamos algo más lejos, se deberian haber implementado _Smoke Tests_, _Monkey Tests_, etc...

* Se podria haber implementado un _Health Check_ para poder saber fácilmente si el servicio se encuentra disponible.

* Se publica la _API_ en __Azure__ sin usar _docker_.
