﻿using Net.Codecrete.QrCodeGenerator;
using SmartHealthCard.QRCode.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCard.QRCode.Encoder
{
  public class QRCodeEncoder : IQRCodeEncoder
  {
    public List<Bitmap> GetQRCodeList(IEnumerable<Chunk> ChunkList, QRCodeEncoderSettings QRCodeEncoderSettings)
    {
      List<Bitmap> BitmapList = new List<Bitmap>();
      foreach (Chunk Chunk in ChunkList)
      {
        List<QrSegment> SegmentList = new List<QrSegment>()
        {
          QrSegment.MakeBytes(Encoding.ASCII.GetBytes(Chunk.ByteSegment)),
          QrSegment.MakeNumeric(Chunk.NumericSegment)
        };
        
        QrCode QrCode = QrCode.EncodeSegments(SegmentList, QrCode.Ecc.Low, 22, 22);
        BitmapList.Add(QrCode.ToBitmap(
          QRCodeEncoderSettings.Scale, 
          QRCodeEncoderSettings.Border, 
          QRCodeEncoderSettings.Foreground, 
          QRCodeEncoderSettings.Background));
      }
      return BitmapList;
    }

    public List<string> GetQRCodeRawDataList(IEnumerable<Chunk> ChunkList)
    {
      List<string> QRCodeData = new List<string>();
      foreach (Chunk Chunk in ChunkList)
      {
        string Data = $"{Chunk.ByteSegment}{Chunk.NumericSegment}";
        QRCodeData.Add(Data);        
      }
      return QRCodeData;
    }
  }
}
