-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.5.8-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Дамп структуры базы данных testtask_vodovoz
CREATE DATABASE IF NOT EXISTS `testtask_vodovoz` /*!40100 DEFAULT CHARACTER SET utf32 COLLATE utf32_unicode_ci */;
USE `testtask_vodovoz`;

-- Дамп структуры для таблица testtask_vodovoz.department
CREATE TABLE IF NOT EXISTS `department` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `description` varchar(64) COLLATE utf32_unicode_ci NOT NULL,
  `executive_id` smallint(5) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_executive` (`executive_id`),
  CONSTRAINT `fk_executive` FOREIGN KEY (`executive_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci;

-- Дамп данных таблицы testtask_vodovoz.department: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
REPLACE INTO `department` (`id`, `description`, `executive_id`) VALUES
	(1, 'Финансовый отдел', 1),
	(4, 'Отдел продаж', 3);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;

-- Дамп структуры для таблица testtask_vodovoz.employee
CREATE TABLE IF NOT EXISTS `employee` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `_surname` varchar(64) COLLATE utf32_unicode_ci NOT NULL,
  `_name` varchar(64) COLLATE utf32_unicode_ci NOT NULL,
  `_fathername` varchar(64) COLLATE utf32_unicode_ci DEFAULT NULL,
  `birthday` datetime DEFAULT NULL,
  `gender` enum('male','female') COLLATE utf32_unicode_ci DEFAULT NULL,
  `department_id` smallint(5) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_department` (`department_id`),
  CONSTRAINT `fk_department` FOREIGN KEY (`department_id`) REFERENCES `department` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci;

-- Дамп данных таблицы testtask_vodovoz.employee: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
REPLACE INTO `employee` (`id`, `_surname`, `_name`, `_fathername`, `birthday`, `gender`, `department_id`) VALUES
	(1, 'Кириллов', 'Кирилл', 'Кириллович', '2021-06-28 14:08:07', 'female', 1),
	(3, 'Петров', 'Петр', 'Олегович', '1111-11-11 00:00:00', 'male', 4);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;

-- Дамп структуры для таблица testtask_vodovoz.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `contragent` varchar(64) COLLATE utf32_unicode_ci NOT NULL,
  `orderdate` datetime DEFAULT NULL,
  `autor_id` smallint(5) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_autor` (`autor_id`),
  CONSTRAINT `fk_autor` FOREIGN KEY (`autor_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf32 COLLATE=utf32_unicode_ci;

-- Дамп данных таблицы testtask_vodovoz.orders: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
REPLACE INTO `orders` (`id`, `contragent`, `orderdate`, `autor_id`) VALUES
	(1, 'Контрагент1', '2021-06-28 14:08:55', 1),
	(3, 'Контрагент2', '1111-11-11 00:00:00', 3);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

-- Дамп структуры для представление testtask_vodovoz.view_department
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `view_department` (
	`id` SMALLINT(5) UNSIGNED NOT NULL,
	`description` VARCHAR(64) NOT NULL COLLATE 'utf32_unicode_ci',
	`_surname` VARCHAR(64) NULL COLLATE 'utf32_unicode_ci',
	`executive_id` SMALLINT(5) UNSIGNED NULL
) ENGINE=MyISAM;

-- Дамп структуры для представление testtask_vodovoz.view_employee
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `view_employee` (
	`id` SMALLINT(5) UNSIGNED NOT NULL,
	`_surname` VARCHAR(64) NOT NULL COLLATE 'utf32_unicode_ci',
	`_name` VARCHAR(64) NOT NULL COLLATE 'utf32_unicode_ci',
	`_fathername` VARCHAR(64) NULL COLLATE 'utf32_unicode_ci',
	`gender` ENUM('male','female') NULL COLLATE 'utf32_unicode_ci',
	`birthday` DATETIME NULL,
	`description` VARCHAR(64) NULL COLLATE 'utf32_unicode_ci'
) ENGINE=MyISAM;

-- Дамп структуры для представление testtask_vodovoz.view_orders
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `view_orders` (
	`id` SMALLINT(5) UNSIGNED NOT NULL,
	`contragent` VARCHAR(64) NOT NULL COLLATE 'utf32_unicode_ci',
	`orderdate` DATETIME NULL,
	`_surname` VARCHAR(64) NOT NULL COLLATE 'utf32_unicode_ci',
	`autor_id` SMALLINT(5) UNSIGNED NULL
) ENGINE=MyISAM;

-- Дамп структуры для представление testtask_vodovoz.view_department
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `view_department`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_department` AS SELECT a.id, a.description, b._surname, a.executive_id FROM department a LEFT JOIN employee b ON a.executive_id = b.id ;

-- Дамп структуры для представление testtask_vodovoz.view_employee
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `view_employee`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_employee` AS SELECT a.id, a._surname, a._name, a._fathername, a.gender, a.birthday, b.description FROM employee a LEFT JOIN department b ON a.department_id = b.id ;

-- Дамп структуры для представление testtask_vodovoz.view_orders
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `view_orders`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_orders` AS SELECT a.id, a.contragent, a.orderdate, b._surname, a.autor_id from orders a INNER JOIN employee b ON a.autor_id = b.id ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
