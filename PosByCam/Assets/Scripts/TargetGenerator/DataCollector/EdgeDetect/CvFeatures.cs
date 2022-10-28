	using OpenCvSharp;
	using UnityEngine;


	public static class CvFeatures {
		public static Mat GetEdges(Mat input, EdgeSettings edgeSettings){
			Mat output = new Mat();

			if (edgeSettings.getRedChannelOnly){
				Cv2.Split(input, out var rgb);
				output = rgb[0];
			}
			else{
				Cv2.CvtColor(input, output, ColorConversionCodes.BGR2GRAY);
			}

			if (edgeSettings.blurEnabled)
				Cv2.GaussianBlur(output, output, new Size(5, 5), 0);

			Cv2.Canny(output, output, edgeSettings.canny1, edgeSettings.canny2);

			if (edgeSettings.thresholdEnabled)
				Cv2.Threshold(output, output, edgeSettings.threshold, edgeSettings.thresholdMax, ThresholdTypes.Binary);

			if (edgeSettings.adaptiveThresholdEnabled)
				Cv2.AdaptiveThreshold(output, output, edgeSettings.adaptiveMax, edgeSettings.adaptiveThresholdType, ThresholdTypes.BinaryInv, edgeSettings.adaptiveBlockSize, 2);

			if (edgeSettings.dilateEnabled)
				Cv2.Dilate(output, output, new Mat(), null, 1);

			return output;
		}


		public static bool TryMatch(Mat templateDilate, Mat cameraDilate, float accuracy, out float successResult){
			Mat matchResult = new Mat();
			Cv2.MatchTemplate(cameraDilate, templateDilate, matchResult, TemplateMatchModes.CCoeffNormed);
			Cv2.MinMaxLoc(matchResult, out double minVal, out double maxVal);

			successResult = (float)maxVal;
			return maxVal > accuracy;
		}
	}
	