# kafka-demo

Demo criada para estudar conceitos básicos de Kafka, mensageria e Event Driven Architecture.

* A demo consiste em dois projetos, Producer e Consumer. 
* O Producer é responsável por criar e remover mensagens, enquanto o Consumer as avalia através de Upvotes e Downvotes.
* Sempre que uma mensagem é criada ou removida pelo Producer, um evento é gerado para o Consumer
* Sempre que uma mensagem é avaliada positivamente ou negativamente pelo Consumer, um evento é gerado para o Producer.

A demo está utilizando .NET 6 com Minimal APIs e MongoDB para armazenamento dos dados.

# Como executar

Em um terminal, executar docker-compose up --build para inicializar os contêiners do kafka e do mongo.
Abrir a solução através do Visual Studio e definir os projetos Consumer.API e Publisher.API como projetos de inicialização. Em seguida, basta executar a solução.

# Disclaimer

Por ser um projeto experimental para aprender kafka, algumas decisões de implementação podem não refletir a melhor forma como alguns itens deveriam ser implementados.
Por exemplo, a forma de armazenar os tópicos ou a relação entre os eventos de um projeto e os commands de outro (e vice versa). 
Também existe muita repetição de código entre ambas as classes pois eu quis tratá-las como módulos independentes, sendo a única coisa compartilhada entre ambas a 
biblioteca Shared, que armazena apenas interfaces, classes abstratas ou recursos indendentes para auxiliar na infraestrutura.
