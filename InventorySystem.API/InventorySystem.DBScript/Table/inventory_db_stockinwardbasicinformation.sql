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
-- Table structure for table `stockinwardbasicinformation`
--

DROP TABLE IF EXISTS `stockinwardbasicinformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stockinwardbasicinformation` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `InvoiceNumber` varchar(32) NOT NULL,
  `PurchaseOrderNumber` varchar(32) DEFAULT NULL,
  `DateOfPurchase` datetime NOT NULL,
  `VendorId` int NOT NULL,
  `MoveTypeId` int NOT NULL,
  `WarehouseId` int NOT NULL,
  `ItemTypeId` int NOT NULL,
  `IsApprovalRequired` tinyint DEFAULT NULL,
  `IsSentForManagerApproval` tinyint(1) DEFAULT NULL,
  `IsApprovedByManager` tinyint DEFAULT NULL,
  `Status` int NOT NULL DEFAULT '0',
  `CreatedBy` int NOT NULL,
  `CreatedOn` datetime DEFAULT CURRENT_TIMESTAMP,
  `ModifiedBy` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `MoveTypeId` (`MoveTypeId`),
  KEY `stockinwardbasicinformation_ibfk_1` (`VendorId`),
  KEY `idx_warehouse_location_id` (`WarehouseId`),
  KEY `ItemTypeId` (`ItemTypeId`),
  CONSTRAINT `stockinwardbasicinformation_ibfk_1` FOREIGN KEY (`VendorId`) REFERENCES `vendor` (`Id`),
  CONSTRAINT `stockinwardbasicinformation_ibfk_2` FOREIGN KEY (`MoveTypeId`) REFERENCES `movementtype` (`Id`),
  CONSTRAINT `stockinwardbasicinformation_ibfk_3` FOREIGN KEY (`WarehouseId`) REFERENCES `warehouse` (`Id`),
  CONSTRAINT `stockinwardbasicinformation_ibfk_4` FOREIGN KEY (`ItemTypeId`) REFERENCES `itemtype` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-07 15:43:09
