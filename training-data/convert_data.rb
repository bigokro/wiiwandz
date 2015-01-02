class Converter

  PIXELS = 20
  
  def self.answer_index answer
    case (answer)
    when 'Aguamenti' then 1
    when 'Alohomora' then 2
    when 'ArrestoMomentum' then 3
    when 'Ascendio' then 4
    when 'Descendo' then 5
    when 'Herbivicus' then 6
    when 'Incendio' then 7
    when 'Locomotor' then 8
    when 'Metelojinx' then 9
    when 'Mimblewimble' then 10
    when 'Reparo' then 11
    when 'Revelio' then 12
    when 'Silencio' then 13
    when 'SpecialisRevelio' then 14
    when 'Tarantallegra' then 15
    when 'WingardiumLeviosa' then 16
    end
  end

  def self.find_max_min points
      xMin = 1023
      xMax = 0
      yMin = 760
      yMax = 0
      points.each_with_index do |point, i|
        x, y = point.split(',')
        x = x.to_i
        y = y.to_i
        xMin = x if x < xMin
        xMax = x if x > xMax
        yMin = y if y < yMin
        yMax = y if y > yMax
      end
      [xMin, xMax, yMin, yMax]
  end

  def self.strings_to_arrays points
      newPoints = []
      points.each_with_index do |point, i|
        x, y = point.split(',')
        x = x.to_i
        y = y.to_i
        newPoints[i] = [x, y]
      end
      newPoints
  end

  def self.scale_point point, xMin, yMin, xScale, yScale
    x = ((point[0]-xMin) * xScale).to_i
    y = ((point[1]-yMin) * yScale).to_i
    [x, y]
  end
  
  def self.set_data_point data, x, y
    x = [x, PIXELS-1].min
    y = [y, PIXELS-1].min
    data[x*PIXELS + y] = 1
  end
end

=begin
line_num=0
text=File.open('xxx.txt').read
text.gsub!(/\r\n?/, "\n")
text.each_line do |line|
  print "#{line_num += 1} #{line}"
end
=end

filenames = [
  'wiiwands-test-data',
  'wiiwands-test-data-training',
  'wiiwands-test-data-testing',
  'wiiwands-test-data-validation'
]

filenames.each do |filename|
  open("#{filename}-oct.txt", 'w') do |f|
    File.readlines("#{filename}.txt").each do |line|
      line.gsub!(/[\r\n]/, "")
      #puts line
      points = line.split(';')
      unless points.empty? or points.size < 3
        answer = Converter.answer_index points[0]
        
        points = points[1..-1]
        
        xMin, xMax, yMin, yMax = Converter.find_max_min points
        points                 = Converter.strings_to_arrays points
        
        #puts points.to_s
        width  = xMax - xMin
        height = yMax - yMin
        xScale = Converter::PIXELS.to_f / width
        yScale = Converter::PIXELS.to_f / height
        
        # Flatten points into array of 1's and 0's
        # 1 = point part of wand movement
        # 0 = point not touched by wand
        data = Array.new(Converter::PIXELS * Converter::PIXELS, 0)
        points[1..-2].each_with_index do |point, i|
          x, y = Converter.scale_point point, xMin, yMin, xScale, yScale
          
          nextPoint = points[i+1]
          nextX, nextY = Converter.scale_point nextPoint, xMin, yMin, xScale, yScale
          
          #puts "point: #{point[0]},#{point[1]} -> #{x},#{y} nextPoint: #{nextPoint[0]},#{nextPoint[1]} -> #{nextX},#{nextY} xMin: #{xMin} yMin: #{yMin} width: #{width} height: #{height} xScale: #{xScale} yScale: #{yScale}" if (x >=Converter::PIXELS or y >= Converter::PIXELS or nextX >= Converter::PIXELS or nextY >= Converter::PIXELS)
          
          # Count any points in between, as if the wand passed in a straight line
          
          Converter.set_data_point data, x, y
          Converter.set_data_point data, nextX, nextY
          if (nextX - x) != 0
            slope = (nextY.to_f - y.to_f)/ (nextX - x)
            (x..nextX).each_with_index do |newX, i|
              newY = (y + (i*slope)).to_i
              Converter.set_data_point data, nexX, newY
            end
          end
          
          if (nextY - y) != 0
            slope = (nextX.to_f - x.to_f)/ (nextY - y)
            (y..nextY).each_with_index do |newY, i|
              newX = (x + (i*slope)).to_i
              Converter.set_data_point data, nexX, newY
            end
          end
        end
        
        # Add start point and end point as factors
        data += points[0]
        data += points[-1]
        
        # Add correct answer
        data += [answer]
        
        f.puts data.to_s[1..-2]
      end
    end
  end
end
