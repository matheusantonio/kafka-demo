# kafka-demo

Demo criada para estudar conceitos básicos de Kafka, mensageria e Event Driven Architecture.

* A demo consiste em dois projetos, Producer e Consumer. 
* O Producer é responsável por criar e remover mensagens, enquanto o Consumer as avalia através de Upvotes e Downvotes.
* Sempre que uma mensagem é criada ou removida pelo Producer, um evento é gerado para o Consumer
* Sempre que uma mensagem é avaliada positivamente ou negativamente pelo Consumer, um evento é gerado para o Producer.

A demo está utilizando .NET 6, as APIs sendo feitas através do novo recurso, Minimal APIs. Pretendo utilizar MongoDB para armazenar os dados mais para frente.

# Disclaimer

Por ser um projeto experimental, algumas decisões de implementação podem não refletir a melhor forma como alguns itens deveriam ser implementados.
Por exemplo, a forma de armazenar os Topics ou a relação entre os eventos de um projeto e os commands de outro (e vice versa). 
Também existe muita repetição de código entre ambas as classes pois eu quis tratá-las como módulos independentes, sendo a única coisa compartilhada entre ambas a 
biblioteca Shared, que armazena apenas interfaces, classes abstratas ou recursos indendentes para auxiliar na infraestrutura.
