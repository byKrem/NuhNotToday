# NuhNotToday

[![ru](https://img.shields.io/badge/lang-ru-red.svg)](README.ru.md)

---

A simple C# .Net application that will be forcing selected executables processes to close when they open.

## Still in development

The application is still not finished and does not meet all requirements, but here are the functions with priority in development:

The application user can select any executable (.exe) file to add it to the list for automatic closure.
It is also possible to select a whole directory (folder) with variations of the selected directory only, or taking into account the directory tree.

When you start any executable from the list, the application should be automatically closed.

It is possible to define the conditions for automatic application closure such as:
* Day-to-day;
* Time frame. For example: 9:00 to 18:00;
* Or permanent "blocking";

Some conditions are also shared.
