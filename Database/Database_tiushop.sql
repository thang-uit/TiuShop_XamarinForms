-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 15, 2022 at 02:48 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tiushop`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `Acc_ID` int(11) NOT NULL,
  `Acc_Username` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Acc_Password` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Acc_Role` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`Acc_ID`, `Acc_Username`, `Acc_Password`, `Acc_Role`) VALUES
(9, 'thangbeo', '135177621185ae82a825332688e9ff90', 0),
(17, 'thanguit', '25f9e794323b453885f5181f1b624d0b', 0);

-- --------------------------------------------------------

--
-- Table structure for table `cart`
--

CREATE TABLE `cart` (
  `Car_ID` int(11) NOT NULL,
  `Use_ID` int(11) NOT NULL,
  `Pro_ID` int(11) NOT NULL,
  `Pro_Size` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Car_Amount` int(11) NOT NULL,
  `Car_Type` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `Cat_ID` int(11) NOT NULL,
  `Cat_Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`Cat_ID`, `Cat_Name`) VALUES
(1, 'Áo thun'),
(2, 'Áo khoác'),
(3, 'Áo sơ mi'),
(4, 'Quần short'),
(5, 'Quần Jean'),
(6, 'Đầm'),
(7, 'Khác');

-- --------------------------------------------------------

--
-- Table structure for table `collections`
--

CREATE TABLE `collections` (
  `Col_ID` int(11) NOT NULL,
  `Col_Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Col_Img` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `collections`
--

INSERT INTO `collections` (`Col_ID`, `Col_Name`, `Col_Img`) VALUES
(1, 'Hangout with friends', '/Code/TiuShop/Assets/Image/Collection/hwf.jpg'),
(2, 'Dating', '/Code/TiuShop/Assets/Image/Collection/dating.jpg'),
(3, 'Party', '/Code/TiuShop/Assets/Image/Collection/party.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `comment`
--

CREATE TABLE `comment` (
  `Com_ID` int(11) NOT NULL,
  `Pro_ID` int(11) NOT NULL,
  `Use_ID` int(11) NOT NULL,
  `Com_Rating` int(11) NOT NULL,
  `Com_Content` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Com_Date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `comment`
--

INSERT INTO `comment` (`Com_ID`, `Pro_ID`, `Use_ID`, `Com_Rating`, `Com_Content`, `Com_Date`) VALUES
(1, 1, 9, 5, 'Áo này đẹp quá', '2022-01-05 11:11:27'),
(9, 3, 17, 5, 'Ao rat dep', '2022-01-15 05:27:45');

-- --------------------------------------------------------

--
-- Table structure for table `orderdetails`
--

CREATE TABLE `orderdetails` (
  `Ord_ID` int(11) NOT NULL,
  `Pro_ID` int(11) NOT NULL,
  `Pro_Size` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Ode_Amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `orderdetails`
--

INSERT INTO `orderdetails` (`Ord_ID`, `Pro_ID`, `Pro_Size`, `Ode_Amount`) VALUES
(6, 1, 'XL', 2),
(6, 3, 'XL', 1),
(7, 4, 'XL', 1),
(8, 3, 'XL', 2),
(8, 4, 'XL', 2),
(8, 5, 'XL', 2),
(9, 4, 'XL', 1),
(9, 5, 'XL', 1),
(10, 3, 'XL', 2);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `Ord_ID` int(11) NOT NULL,
  `Use_ID` int(11) NOT NULL,
  `Ord_Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Ord_Email` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Ord_Phone` varchar(15) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Ord_Address` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Ord_Note` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `Ord_Payment` int(11) NOT NULL DEFAULT 0,
  `Ord_Date` datetime NOT NULL DEFAULT current_timestamp(),
  `Ord_Date_Success` datetime NOT NULL DEFAULT current_timestamp(),
  `Ord_Status` int(11) NOT NULL DEFAULT 0,
  `Ord_Total` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`Ord_ID`, `Use_ID`, `Ord_Name`, `Ord_Email`, `Ord_Phone`, `Ord_Address`, `Ord_Note`, `Ord_Payment`, `Ord_Date`, `Ord_Date_Success`, `Ord_Status`, `Ord_Total`) VALUES
(6, 9, 'Chu Nam Thắng', 'namthangmu123@gmail.com', '0961600587', 'Long Thanh, Dong Nai', 'Abc', 0, '2022-01-14 04:05:51', '2022-01-15 05:30:11', 3, 2814000),
(7, 9, 'Chu Nam Thắng', 'namthangmu123@gmail.com', '0961600587', 'Long Thanh, Dong Nai', 'def', 0, '2022-01-14 16:14:02', '2022-01-15 05:30:12', 3, 1450000),
(8, 9, 'Chu Nam Thắng', 'namthangmu123@gmail.com', '0961600587', 'Long Thanh, Dong Nai', 'ghi', 0, '2022-01-14 16:55:13', '2022-01-15 04:39:04', 3, 5960000),
(9, 17, 'Chu Nam Thang', 'namthang@gmail.com', '0961600587', 'Ki tuc xá khu B DHQG TPHCM', 'Cong sau khu B', 0, '2022-01-15 05:22:46', '2022-01-15 05:22:46', 4, 1840000),
(10, 17, 'Chu Nam Thang', 'namthang@gmail.com', '0961600587', 'Ki tuc xá khu B DHQG TPHCM', 'Cong sau khu B', 0, '2022-01-15 05:24:41', '2022-01-15 05:26:33', 3, 2280000);

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `Pro_ID` int(11) NOT NULL,
  `Pro_Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Pro_Price` int(11) NOT NULL,
  `Pro_Sale` int(11) NOT NULL DEFAULT 0,
  `Pro_Description` varchar(1000) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.',
  `Pro_Date` datetime NOT NULL DEFAULT current_timestamp(),
  `Pro_Gender` int(11) NOT NULL DEFAULT 2,
  `Cat_ID` int(11) NOT NULL,
  `Col_ID` int(11) NOT NULL,
  `Pro_Stock` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`Pro_ID`, `Pro_Name`, `Pro_Price`, `Pro_Sale`, `Pro_Description`, `Pro_Date`, `Pro_Gender`, `Cat_ID`, `Col_ID`, `Pro_Stock`) VALUES
(1, 'BỘ HOMEWEAR KẺ CARO', 900000, 7, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-04 10:24:18', 2, 3, 3, 20),
(3, 'ÁO KHOÁC BOMBER QUILTING', 1200000, 5, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-04 10:36:38', 0, 2, 1, 50),
(4, 'ÁO BLAZER TEXTURE. FITTED', 1450000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-04 11:12:16', 0, 7, 2, 50),
(5, 'ÁO BẺ LÁ CỔ 1 NÚT', 390000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-04 11:23:53', 1, 7, 1, 50),
(6, 'ÁO THUN TAY NGẮN', 250000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-04 11:29:48', 0, 1, 1, 50),
(7, 'QUẦN JEAN NỮ. LOOSE FORM', 480000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 22:42:10', 1, 5, 3, 50),
(8, 'QUẦN SHORT KAKI. SLIM FORM', 450000, 5, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 23:37:38', 0, 4, 1, 50),
(9, 'ÁO KHOÁC CARO.REGULAR', 950000, 10, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 23:42:12', 0, 2, 1, 50),
(10, 'QUẦN JEAN TRƠN. SLIM FIT FORM', 650000, 8, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 23:48:20', 0, 5, 2, 50),
(11, 'ÁO SƠ MI TAY DÀI, KẺ SỌC. REGULAR FORM', 450000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 23:51:45', 0, 3, 3, 50),
(12, 'QUẦN SHORT NỮ', 395000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-14 23:55:39', 1, 4, 1, 50),
(13, 'ÁO SƠ MI TAY DÀI, XẾP LY SAU', 680000, 7, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:00:10', 1, 3, 3, 50),
(14, 'ĐẦM TAY CON THẮT BELT', 1000000, 10, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:06:51', 1, 6, 2, 50),
(15, 'ĐẦM CỔ V NHÚN VAI', 750000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:09:55', 1, 6, 1, 50),
(16, 'ĐẦM CỔ VUÔNG XẾP LY XÉO', 780000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:12:43', 1, 6, 2, 50),
(17, 'ÁO THUN TAY NGẮN, IN CHỮ', 350000, 5, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:15:35', 1, 1, 1, 50),
(18, 'ÁO THUN TAY NGẮN THÊU CHỮ NGỰC', 220000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:22:49', 1, 1, 1, 50),
(19, 'QUẦN JEAN TƯA LAI. SLIM FORM', 395000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:26:01', 2, 5, 1, 50),
(20, 'ÁO KHOÁC CORDOUROY.REGULAR', 1300000, 5, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:31:42', 0, 2, 1, 50),
(21, 'QUẦN SHORT TRƠN. COTTON. STRAIGHT FORM', 350000, 0, 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', '2022-01-15 00:36:17', 0, 4, 1, 50);

-- --------------------------------------------------------

--
-- Table structure for table `productimg`
--

CREATE TABLE `productimg` (
  `Pim_ID` int(11) NOT NULL,
  `Pro_ID` int(11) NOT NULL,
  `Pim_Img` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `productimg`
--

INSERT INTO `productimg` (`Pim_ID`, `Pro_ID`, `Pim_Img`) VALUES
(1, 1, '/Code/TiuShop/Assets/Image/Product/1/0.jpg'),
(2, 1, '/Code/TiuShop/Assets/Image/Product/1/1.jpg'),
(3, 1, '/Code/TiuShop/Assets/Image/Product/1/2.jpg'),
(4, 1, '/Code/TiuShop/Assets/Image/Product/1/3.jpg'),
(5, 1, '/Code/TiuShop/Assets/Image/Product/1/4.jpg'),
(6, 3, '/Code/TiuShop/Assets/Image/Product/2/0.jpg'),
(7, 3, '/Code/TiuShop/Assets/Image/Product/2/1.jpg'),
(8, 3, '/Code/TiuShop/Assets/Image/Product/2/2.jpg'),
(9, 3, '/Code/TiuShop/Assets/Image/Product/2/3.jpg'),
(10, 3, '/Code/TiuShop/Assets/Image/Product/2/4.jpg'),
(11, 4, '/Code/TiuShop/Assets/Image/Product/3/0.jpg'),
(12, 4, '/Code/TiuShop/Assets/Image/Product/3/1.jpg'),
(13, 4, '/Code/TiuShop/Assets/Image/Product/3/2.jpg'),
(14, 4, '/Code/TiuShop/Assets/Image/Product/3/3.jpg'),
(15, 4, '/Code/TiuShop/Assets/Image/Product/3/4.jpg'),
(16, 4, '/Code/TiuShop/Assets/Image/Product/3/5.jpg'),
(17, 5, '/Code/TiuShop/Assets/Image/Product/4/0.jpg'),
(18, 5, '/Code/TiuShop/Assets/Image/Product/4/1.jpg'),
(19, 5, '/Code/TiuShop/Assets/Image/Product/4/2.jpg'),
(20, 5, '/Code/TiuShop/Assets/Image/Product/4/3.jpg'),
(21, 5, '/Code/TiuShop/Assets/Image/Product/4/4.jpg'),
(22, 6, '/Code/TiuShop/Assets/Image/Product/5/0.jpg'),
(23, 6, '/Code/TiuShop/Assets/Image/Product/5/1.jpg'),
(24, 6, '/Code/TiuShop/Assets/Image/Product/5/2.jpg'),
(25, 6, '/Code/TiuShop/Assets/Image/Product/5/3.jpg'),
(26, 6, '/Code/TiuShop/Assets/Image/Product/5/4.jpg'),
(27, 7, '/Code/TiuShop/Assets/Image/Product/6/0.jpg'),
(28, 7, '/Code/TiuShop/Assets/Image/Product/6/1.jpg'),
(29, 7, '/Code/TiuShop/Assets/Image/Product/6/2.jpg'),
(30, 7, '/Code/TiuShop/Assets/Image/Product/6/3.jpg'),
(31, 7, '/Code/TiuShop/Assets/Image/Product/6/4.jpg'),
(32, 8, '/Code/TiuShop/Assets/Image/Product/7/0.jpg'),
(33, 8, '/Code/TiuShop/Assets/Image/Product/7/1.jpg'),
(34, 8, '/Code/TiuShop/Assets/Image/Product/7/2.jpg'),
(35, 8, '/Code/TiuShop/Assets/Image/Product/7/3.jpg'),
(36, 8, '/Code/TiuShop/Assets/Image/Product/7/4.jpg'),
(37, 8, '/Code/TiuShop/Assets/Image/Product/7/5.jpg'),
(38, 9, '/Code/TiuShop/Assets/Image/Product/8/0.jpg'),
(39, 9, '/Code/TiuShop/Assets/Image/Product/8/1.jpg'),
(40, 9, '/Code/TiuShop/Assets/Image/Product/8/2.jpg'),
(41, 9, '/Code/TiuShop/Assets/Image/Product/8/3.jpg'),
(42, 9, '/Code/TiuShop/Assets/Image/Product/8/4.jpg'),
(43, 9, '/Code/TiuShop/Assets/Image/Product/8/5.jpg'),
(44, 10, '/Code/TiuShop/Assets/Image/Product/9/0.jpg'),
(45, 10, '/Code/TiuShop/Assets/Image/Product/9/1.jpg'),
(46, 10, '/Code/TiuShop/Assets/Image/Product/9/2.jpg'),
(47, 10, '/Code/TiuShop/Assets/Image/Product/9/3.jpg'),
(48, 10, '/Code/TiuShop/Assets/Image/Product/9/4.jpg'),
(49, 11, '/Code/TiuShop/Assets/Image/Product/10/0.jpg'),
(50, 11, '/Code/TiuShop/Assets/Image/Product/10/1.jpg'),
(51, 11, '/Code/TiuShop/Assets/Image/Product/10/2.jpg'),
(52, 11, '/Code/TiuShop/Assets/Image/Product/10/3.jpg'),
(53, 11, '/Code/TiuShop/Assets/Image/Product/10/4.jpg'),
(54, 11, '/Code/TiuShop/Assets/Image/Product/10/5.jpg'),
(55, 12, '/Code/TiuShop/Assets/Image/Product/11/0.jpg'),
(56, 12, '/Code/TiuShop/Assets/Image/Product/11/1.jpg'),
(57, 12, '/Code/TiuShop/Assets/Image/Product/11/2.jpg'),
(58, 12, '/Code/TiuShop/Assets/Image/Product/11/3.jpg'),
(59, 12, '/Code/TiuShop/Assets/Image/Product/11/4.jpg'),
(60, 12, '/Code/TiuShop/Assets/Image/Product/11/5.jpg'),
(61, 13, '/Code/TiuShop/Assets/Image/Product/12/0.jpg'),
(62, 13, '/Code/TiuShop/Assets/Image/Product/12/1.jpg'),
(63, 13, '/Code/TiuShop/Assets/Image/Product/12/2.jpg'),
(64, 13, '/Code/TiuShop/Assets/Image/Product/12/3.jpg'),
(65, 13, '/Code/TiuShop/Assets/Image/Product/12/4.jpg'),
(66, 13, '/Code/TiuShop/Assets/Image/Product/12/5.jpg'),
(67, 14, '/Code/TiuShop/Assets/Image/Product/13/0.jpg'),
(68, 14, '/Code/TiuShop/Assets/Image/Product/13/1.jpg'),
(69, 14, '/Code/TiuShop/Assets/Image/Product/13/2.jpg'),
(70, 14, '/Code/TiuShop/Assets/Image/Product/13/3.jpg'),
(71, 14, '/Code/TiuShop/Assets/Image/Product/13/4.jpg'),
(72, 15, '/Code/TiuShop/Assets/Image/Product/14/0.jpg'),
(73, 15, '/Code/TiuShop/Assets/Image/Product/14/1.jpg'),
(74, 15, '/Code/TiuShop/Assets/Image/Product/14/2.jpg'),
(75, 15, '/Code/TiuShop/Assets/Image/Product/14/3.jpg'),
(76, 15, '/Code/TiuShop/Assets/Image/Product/14/4.jpg'),
(77, 16, '/Code/TiuShop/Assets/Image/Product/15/0.jpg'),
(78, 16, '/Code/TiuShop/Assets/Image/Product/15/1.jpg'),
(79, 16, '/Code/TiuShop/Assets/Image/Product/15/2.jpg'),
(80, 16, '/Code/TiuShop/Assets/Image/Product/15/3.jpg'),
(81, 16, '/Code/TiuShop/Assets/Image/Product/15/4.jpg'),
(82, 17, '/Code/TiuShop/Assets/Image/Product/16/0.jpg'),
(83, 17, '/Code/TiuShop/Assets/Image/Product/16/1.jpg'),
(84, 17, '/Code/TiuShop/Assets/Image/Product/16/2.jpg'),
(85, 17, '/Code/TiuShop/Assets/Image/Product/16/3.jpg'),
(86, 17, '/Code/TiuShop/Assets/Image/Product/16/4.jpg'),
(87, 18, '/Code/TiuShop/Assets/Image/Product/17/0.jpg'),
(88, 18, '/Code/TiuShop/Assets/Image/Product/17/1.jpg'),
(89, 18, '/Code/TiuShop/Assets/Image/Product/17/2.jpg'),
(90, 18, '/Code/TiuShop/Assets/Image/Product/17/3.jpg'),
(91, 18, '/Code/TiuShop/Assets/Image/Product/17/4.jpg'),
(92, 18, '/Code/TiuShop/Assets/Image/Product/17/5.jpg'),
(93, 19, '/Code/TiuShop/Assets/Image/Product/18/0.jpg'),
(94, 19, '/Code/TiuShop/Assets/Image/Product/18/1.jpg'),
(95, 19, '/Code/TiuShop/Assets/Image/Product/18/2.jpg'),
(96, 19, '/Code/TiuShop/Assets/Image/Product/18/3.jpg'),
(97, 19, '/Code/TiuShop/Assets/Image/Product/18/4.jpg'),
(98, 19, '/Code/TiuShop/Assets/Image/Product/18/5.jpg'),
(99, 20, '/Code/TiuShop/Assets/Image/Product/19/0.jpg'),
(100, 20, '/Code/TiuShop/Assets/Image/Product/19/1.jpg'),
(101, 20, '/Code/TiuShop/Assets/Image/Product/19/2.jpg'),
(102, 20, '/Code/TiuShop/Assets/Image/Product/19/3.jpg'),
(103, 20, '/Code/TiuShop/Assets/Image/Product/19/4.jpg'),
(104, 21, '/Code/TiuShop/Assets/Image/Product/20/0.jpg'),
(105, 21, '/Code/TiuShop/Assets/Image/Product/20/1.jpg'),
(106, 21, '/Code/TiuShop/Assets/Image/Product/20/2.jpg'),
(107, 21, '/Code/TiuShop/Assets/Image/Product/20/3.jpg'),
(108, 21, '/Code/TiuShop/Assets/Image/Product/20/4.jpg'),
(109, 21, '/Code/TiuShop/Assets/Image/Product/20/5.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `slider`
--

CREATE TABLE `slider` (
  `Sli_ID` int(11) NOT NULL,
  `Sli_Img` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Pro_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `slider`
--

INSERT INTO `slider` (`Sli_ID`, `Sli_Img`, `Pro_ID`) VALUES
(1, '/Code/TiuShop/Assets/Image/Slider/slider1.jpg', 1),
(2, '/Code/TiuShop/Assets/Image/Slider/slider2.jpg', 3),
(3, '/Code/TiuShop/Assets/Image/Slider/slider3.jpg', 4),
(4, '/Code/TiuShop/Assets/Image/Slider/slider4.jpg', 5),
(5, '/Code/TiuShop/Assets/Image/Slider/slider5.jpg', 6);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `Use_ID` int(11) NOT NULL,
  `Use_Name` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Use_Email` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Use_Gender` tinyint(1) DEFAULT NULL,
  `Use_Phone` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Use_Address` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Use_Create` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`Use_ID`, `Use_Name`, `Use_Email`, `Use_Gender`, `Use_Phone`, `Use_Address`, `Use_Create`) VALUES
(9, 'Chu Nam Thắng', 'namthangmu123@gmail.com', 0, '0961600587', 'Long Thanh, Dong Nai', '2022-01-02 17:33:47'),
(17, 'Chu Nam Thang', 'namthang@gmail.com', 0, '0961600587', 'Ki tuc xá khu B DHQG TPHCM', '2022-01-15 05:09:25');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`Acc_ID`),
  ADD UNIQUE KEY `Acc_Username` (`Acc_Username`);

--
-- Indexes for table `cart`
--
ALTER TABLE `cart`
  ADD PRIMARY KEY (`Car_ID`),
  ADD KEY `FK_PROID_04` (`Pro_ID`),
  ADD KEY `FK_USEID_04` (`Use_ID`);

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`Cat_ID`);

--
-- Indexes for table `collections`
--
ALTER TABLE `collections`
  ADD PRIMARY KEY (`Col_ID`);

--
-- Indexes for table `comment`
--
ALTER TABLE `comment`
  ADD PRIMARY KEY (`Com_ID`),
  ADD KEY `FK_PROID_03` (`Pro_ID`),
  ADD KEY `FK_USEID_03` (`Use_ID`);

--
-- Indexes for table `orderdetails`
--
ALTER TABLE `orderdetails`
  ADD PRIMARY KEY (`Ord_ID`,`Pro_ID`,`Pro_Size`),
  ADD KEY `FK_PROID_05` (`Pro_ID`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`Ord_ID`),
  ADD KEY `FK_USEID_05` (`Use_ID`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`Pro_ID`),
  ADD KEY `FK_CATID_01` (`Cat_ID`),
  ADD KEY `FK_COLID_01` (`Col_ID`);

--
-- Indexes for table `productimg`
--
ALTER TABLE `productimg`
  ADD PRIMARY KEY (`Pim_ID`),
  ADD KEY `FK_PROID_02` (`Pro_ID`);

--
-- Indexes for table `slider`
--
ALTER TABLE `slider`
  ADD PRIMARY KEY (`Sli_ID`),
  ADD KEY `FK_PROID_01` (`Pro_ID`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`Use_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `Acc_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `cart`
--
ALTER TABLE `cart`
  MODIFY `Car_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=120;

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `Cat_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `collections`
--
ALTER TABLE `collections`
  MODIFY `Col_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `comment`
--
ALTER TABLE `comment`
  MODIFY `Com_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `Ord_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `Pro_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `productimg`
--
ALTER TABLE `productimg`
  MODIFY `Pim_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=110;

--
-- AUTO_INCREMENT for table `slider`
--
ALTER TABLE `slider`
  MODIFY `Sli_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cart`
--
ALTER TABLE `cart`
  ADD CONSTRAINT `FK_PROID_04` FOREIGN KEY (`Pro_ID`) REFERENCES `product` (`Pro_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_USEID_04` FOREIGN KEY (`Use_ID`) REFERENCES `user` (`Use_ID`) ON DELETE CASCADE;

--
-- Constraints for table `comment`
--
ALTER TABLE `comment`
  ADD CONSTRAINT `FK_PROID_03` FOREIGN KEY (`Pro_ID`) REFERENCES `product` (`Pro_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_USEID_03` FOREIGN KEY (`Use_ID`) REFERENCES `user` (`Use_ID`) ON DELETE CASCADE;

--
-- Constraints for table `orderdetails`
--
ALTER TABLE `orderdetails`
  ADD CONSTRAINT `FK_ORDID_01` FOREIGN KEY (`Ord_ID`) REFERENCES `orders` (`Ord_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_PROID_05` FOREIGN KEY (`Pro_ID`) REFERENCES `product` (`Pro_ID`) ON DELETE CASCADE;

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_USEID_05` FOREIGN KEY (`Use_ID`) REFERENCES `user` (`Use_ID`) ON DELETE CASCADE;

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `FK_CATID_01` FOREIGN KEY (`Cat_ID`) REFERENCES `category` (`Cat_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_COLID_01` FOREIGN KEY (`Col_ID`) REFERENCES `collections` (`Col_ID`) ON DELETE CASCADE;

--
-- Constraints for table `productimg`
--
ALTER TABLE `productimg`
  ADD CONSTRAINT `FK_PROID_02` FOREIGN KEY (`Pro_ID`) REFERENCES `product` (`Pro_ID`) ON DELETE CASCADE;

--
-- Constraints for table `slider`
--
ALTER TABLE `slider`
  ADD CONSTRAINT `FK_PROID_01` FOREIGN KEY (`Pro_ID`) REFERENCES `product` (`Pro_ID`) ON DELETE CASCADE;

--
-- Constraints for table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `FK_USEID_01` FOREIGN KEY (`Use_ID`) REFERENCES `account` (`Acc_ID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
