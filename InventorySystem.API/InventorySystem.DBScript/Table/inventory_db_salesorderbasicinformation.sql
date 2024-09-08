-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 103.178.248.62    Database: inventory_db
-- ------------------------------------------------------
-- Server version	8.0.34-0ubuntu0.22.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `salesorderbasicinformation`
--

DROP TABLE IF EXISTS `salesorderbasicinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `salesorderbasicinformation` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SalesOrderNumber` varchar(64) DEFAULT NULL,
  `DateofSale` datetime DEFAULT NULL,
  `VendorId` int DEFAULT NULL,
  `MovementTypeId` int DEFAULT NULL,
  `WarehouseId` int DEFAULT NULL,
  `SaleOrderStatusId` int DEFAULT NULL,
  `OutTypeId` int DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT '0',
  `CreatedBy` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT CURRENT_TIMESTAMP,
  `ModifiedBy` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `WarehouseId` (`WarehouseId`),
  KEY `CustomerId` (`VendorId`),
  KEY `SaleOrderStatusId` (`SaleOrderStatusId`),
  KEY `salesorderbasicinformation_ibfk_1_idx` (`MovementTypeId`),
  KEY `OutType` (`OutTypeId`),
  CONSTRAINT `salesorderbasicinformation_ibfk_1` FOREIGN KEY (`MovementTypeId`) REFERENCES `salesordermovementtype` (`Id`),
  CONSTRAINT `salesorderbasicinformation_ibfk_4` FOREIGN KEY (`WarehouseId`) REFERENCES `warehouse` (`Id`),
  CONSTRAINT `salesorderbasicinformation_ibfk_5` FOREIGN KEY (`VendorId`) REFERENCES `vendor` (`Id`),
  CONSTRAINT `salesorderbasicinformation_ibfk_6` FOREIGN KEY (`SaleOrderStatusId`) REFERENCES `saleorderstatus` (`Id`),
  CONSTRAINT `salesorderbasicinformation_ibfk_7` FOREIGN KEY (`OutTypeId`) REFERENCES `outtype` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-07 15:43:15
