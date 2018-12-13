-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Dec 14, 2018 at 12:23 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `library`
--

-- --------------------------------------------------------

--
-- Table structure for table `authors`
--

CREATE TABLE `authors` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `authors`
--

INSERT INTO `authors` (`id`, `name`) VALUES
(6, 'Leo Tolstoy'),
(7, 'Charles Darwin'),
(8, 'JRR Tolkien'),
(9, 'Tolstoy Jr Jr '),
(10, 'Albert Camus'),
(11, 'Jaroslav Hasek'),
(12, 'Bohumil Hrabal'),
(13, 'Cormack McCarthy');

-- --------------------------------------------------------

--
-- Table structure for table `authors_books`
--

CREATE TABLE `authors_books` (
  `id` int(11) NOT NULL,
  `author_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `authors_books`
--

INSERT INTO `authors_books` (`id`, `author_id`, `book_id`) VALUES
(6, 6, 7),
(7, 7, 8),
(8, 7, 9),
(9, 8, 10),
(10, 9, 13),
(11, 8, 14),
(12, 11, 17),
(13, 12, 18),
(14, 10, 16),
(15, 6, 17),
(16, 7, 19),
(17, 13, 20),
(18, 13, 21),
(19, 6, 12),
(20, 7, 11),
(21, 8, 15),
(22, 6, 16);

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`id`, `name`) VALUES
(11, 'Origin of Species'),
(12, 'War and Peace'),
(13, 'Gone with the Wind'),
(14, 'LOTR'),
(15, 'Hobbit '),
(16, 'The Stranger'),
(17, 'The Good Soldier Svejk'),
(18, 'I Served the King of England'),
(19, 'Origin of Species II'),
(20, 'The Illusionist'),
(21, 'Borderland');

-- --------------------------------------------------------

--
-- Table structure for table `copies`
--

CREATE TABLE `copies` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `author` varchar(255) NOT NULL,
  `checked_out` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `copies`
--

INSERT INTO `copies` (`id`, `name`, `author`, `checked_out`) VALUES
(9, 'Origin of Species', 'Charles Darwin', 1),
(10, 'War and Peace', 'Leo Tolstoy', 0),
(11, 'Origin of Species', 'Charles Darwin', 0),
(12, 'Gone with the Wind', 'Tolstoy Jr Jr ', 0),
(13, 'LOTR', 'JRR Tolkien', 1),
(14, 'LOTR', 'JRR Tolkien', 1),
(15, 'LOTR', 'JRR Tolkien', 1),
(16, 'Hobbit ', 'JRR Tolkien', 0),
(17, 'LOTR', 'JRR Tolkien', 0),
(18, 'The Stranger', 'Albert Camus', 0),
(19, 'The Good Soldier Svejk', 'Jaroslav Hasek', 0),
(20, 'I Served the King of England', 'Bohumil Hrabal', 0),
(21, 'Origin of Species II', 'Charles Darwin', 0),
(22, 'Origin of Species II', 'Charles Darwin', 0),
(23, 'The Illusionist', 'Cormack McCarthy', 0),
(24, 'Borderland', 'Cormack McCarthy', 0);

-- --------------------------------------------------------

--
-- Table structure for table `patrons`
--

CREATE TABLE `patrons` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `patrons`
--

INSERT INTO `patrons` (`id`, `name`) VALUES
(1, 'Johnny Rotten'),
(2, 'Syd Vicious'),
(3, 'Elvis'),
(4, 'Alex Williams');

-- --------------------------------------------------------

--
-- Table structure for table `patrons_copies`
--

CREATE TABLE `patrons_copies` (
  `id` int(11) NOT NULL,
  `patron_id` int(11) NOT NULL,
  `copy_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `patrons_copies`
--

INSERT INTO `patrons_copies` (`id`, `patron_id`, `copy_id`) VALUES
(66, 1, 9),
(67, 4, 13),
(68, 4, 14),
(69, 4, 15);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `authors`
--
ALTER TABLE `authors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `authors_books`
--
ALTER TABLE `authors_books`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `copies`
--
ALTER TABLE `copies`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `patrons`
--
ALTER TABLE `patrons`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `patrons_copies`
--
ALTER TABLE `patrons_copies`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `authors`
--
ALTER TABLE `authors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `authors_books`
--
ALTER TABLE `authors_books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;
--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT for table `copies`
--
ALTER TABLE `copies`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `patrons`
--
ALTER TABLE `patrons`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `patrons_copies`
--
ALTER TABLE `patrons_copies`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=71;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
