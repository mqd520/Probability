/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.0.91
 Source Server Type    : MySQL
 Source Server Version : 80029
 Source Host           : 192.168.0.91:3306
 Source Schema         : Probability

 Target Server Type    : MySQL
 Target Server Version : 80029
 File Encoding         : 65001

 Date: 14/06/2022 09:06:11
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for probability_records
-- ----------------------------
DROP TABLE IF EXISTS `probability_records`;
CREATE TABLE `probability_records`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `expected_probability` float NOT NULL,
  `actual_probability` float NOT NULL,
  `probability_difference` float NOT NULL,
  `count` int NOT NULL,
  `start_time` bigint NOT NULL,
  `end_time` bigint NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2086658 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
