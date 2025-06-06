# Модуль программного обеспечения автоматизации работы логистической компании
Модуль преднозначен для автоматизации и оптимизации логистических операций.\
\
![ChatGPT](https://img.shields.io/badge/chatGPT-74aa9c?style=for-the-badge&logo=openai&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![XAML](https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-68217A?style=for-the-badge&logo=windows&logoColor=white)

## Содержание
- [Введение](#введение)
- [Установка](#установка)
- [Используемые библиотеки](#используемые-библиотеки)
- [База данных](#база-данных)
- [XML Документация](#xml-документация)
- [Диаграммы сущностей](#диаграммы-сущностей)
- [Инструкция](#инструкция)

## Введение
Программный Модуль предназначен  для автоматизации и оптимизации логистических операций а также учет заказов и товара на складе 

## Установка
для установки в **`Bash`** или **`Power Shell`** введите команду: 
```
git clone https://github.com/Raksha228/Logistics_Software.git
```

## Используемые библиотеки
* [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager/)
* [System.Data.SqlClient](https://www.nuget.org/packages/System.Data.SqlClient/)

## База данных
В проекте есть возможность использовать такие варианты как **БД** или **MS Sql Server**

## XML Документация
Ссылка на документацию
> [Frontend](/XML/Frontend.xml)
> [Backend](/XML/Backend.xml)

## Диаграммы сущностей
![Диаграмма класов]()

## Инструкция
Краткое руководство использования:
> [!IMPORTANT]
> 1. Скачать и установить программу Visual Studio 2019.
> 2. Клонируйте проект с местного репозиторя.
> 3. Установка на локальный сервер SQL Express, или другой Microsoft SQL Server.
> 4. Скачать и установить программу SQL Managment Studio.
> 5. Запустите программу MS SQL и подключитесь к локальному серверу на компьютере.
> 6. Импортируйте базу данных (storedatabase), который находится в каталоге на клонируемом репозитории.
> 7. Запустите VS и загрузите проект.
> 8. Откройте вкладку Server Explorer->(right click)Data Connection->Add Connection пишите имя вашего локального сервера, что можно увидеть в SQl Managment Studio на вкладке справа.
> 9. Затем добавьте базу данных (Logistics_Software).
> 10. Далее Откройте app.config файл на Frontend и Backend. На против " connectionString="Data Source=' записывается название вашего локального сервера.
> 11. Если желаете зайти как админ вводите user= admin, password= admin. Как пользователь user= user, password= user. (Это базовые пользователи).
> Следуйте инструкциям для корректной работы приложения.
