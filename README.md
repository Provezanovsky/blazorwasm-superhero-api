# BlazorWasmSuperHeroApi

Atividade em C# e .NET 8 com Blazor WebAssembly. A página inicial busca heróis e vilões por nome e exibe nome, identidade, editora, primeira aparição, imagem e seis atributos dos personagens retornados.

## Como executar

Com o SDK .NET 8 instalado, abra um terminal na raiz do projeto e execute:

```bash
dotnet run --launch-profile http
```

Abra **http://localhost:5197**. Para consultar a API, gere um token na [SuperHero API](https://www.superheroapi.com/) com a opção **Login with GitHub**. Cole o token no formulário, informe o nome de um personagem e clique em **Pesquisar**.

O botão **Ver exemplo** carrega os três resultados de Batman publicados na documentação da API a partir de um JSON incluído no projeto. Esse exemplo pode ser apresentado sem token.

## Integração

O aplicativo envia a consulta HTTP diretamente do navegador para `https://www.superheroapi.com/api.php/{token}/search/{nome}`. O endereço com `www` e `api.php` evita o redirecionamento demonstrado na aula.

O token identifica as requisições que têm acesso à API. Ele é informado no formulário a cada sessão e aparece na requisição HTTP do próprio navegador. Não coloque seu token no código, no README ou em um commit.

- `Pages/Home.razor` controla a busca e os estados de carregamento, erro e resultado vazio.
- `Services/SuperHeroService.cs` realiza as chamadas HTTP e desserializa o JSON.
- `Models/` mapeia os dados retornados pela API.
- `Components/HeroCard.razor` exibe cada personagem e trata fotos indisponíveis.
